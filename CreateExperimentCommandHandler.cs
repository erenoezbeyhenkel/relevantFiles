using ErrorOr;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Common;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetById;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Experiments.Experiment.Create;

public sealed class CreateExperimentCommandHandler(IUnitOfWork unitOfWork,
                                                   ISender sender,
                                                   IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateExperimentCommand, CreateExperimentCommandResponse>
{
    public async Task<ErrorOr<CreateExperimentCommandResponse>> Handle(CreateExperimentCommand request, CancellationToken cancellationToken)
    {
        var experimentCandidate = await unitOfWork.Experiments.FindOneAsync(e => e.Id == request.CreateExperimentDto.ExperimentTemplateId,
            e => e.Include(e => e.MonitorSetups)
                  .ThenInclude(ms => ms.WashCycleSetups)
                  .Include(e => e.TemplateSetting)
                  .Include(e => e.TemplateAdditives), cancellationToken);

        experimentCandidate.OrderId = request.CreateExperimentDto.OrderId;
        experimentCandidate.TemplateExperimentId = experimentCandidate.Id;
        experimentCandidate.Id = 0;

        experimentCandidate.MonitorSetups?.ToList().ForEach(x =>
        {
            x.Id = 0;
            x.ExperimentId = 0;
            x.WashCycleSetups?.ToList().ForEach(w =>
            {
                w.Id = 0;
            });
        });

        //Create product setups based on the selected products and product dependent template additives(if any.)
        var productSetups = new List<Domain.Entities.Experiments.ProductSetup>();
        var productDependentTemplateAdditives = experimentCandidate.TemplateAdditives.Where(psa => psa.IsProductDependent).ToList();
        var productIndependentTemplateAdditives = experimentCandidate.TemplateAdditives.Where(psa => !psa.IsProductDependent).ToList();
        var position = 1;

        //Product independent additives        

        if (Guard.Against.IsAnyOrNotEmpty(request.CreateExperimentDto.Products))
        {
            request.CreateExperimentDto.Products.ToList().ForEach(product =>
            {
                var productSetupAdditives = new List<Domain.Entities.Experiments.ProductSetupAdditive>();
                productIndependentTemplateAdditives.ForEach(productIndependentTemplateAdditive =>
                {
                    productSetupAdditives.Add(new Domain.Entities.Experiments.ProductSetupAdditive
                    {
                        AdditiveTypeId = productIndependentTemplateAdditive.AdditiveTypeId,
                        Amount = productIndependentTemplateAdditive.Amount,
                        IsProductDependent = productIndependentTemplateAdditive.IsProductDependent
                    });
                });

                productSetups.Add(new Domain.Entities.Experiments.ProductSetup
                {
                    WaterLevel = experimentCandidate.TemplateSetting.WaterLevel,
                    WaterHardness = experimentCandidate.TemplateSetting.WaterHardness,
                    ChlorLevel = experimentCandidate.TemplateSetting.ChlorLevel,
                    MixingRatio = experimentCandidate.TemplateSetting.MixingRatio,
                    RotationTime = experimentCandidate.TemplateSetting.RotationTime,
                    Drumspeed = experimentCandidate.TemplateSetting.Drumspeed,
                    Position = position,
                    ProductId = product.Id,
                    Additives = [.. productSetupAdditives]

                });

                position++;
                if (Guard.Against.IsAnyOrNotEmpty(productDependentTemplateAdditives))
                {
                    productDependentTemplateAdditives.ForEach(productDependentTemplateAdditive =>
                    {
                        var dependentProductSetupAdditives = new List<Domain.Entities.Experiments.ProductSetupAdditive>();
                        productIndependentTemplateAdditives.ForEach(productIndependentTemplateAdditive =>
                        {
                            dependentProductSetupAdditives.Add(new Domain.Entities.Experiments.ProductSetupAdditive
                            {
                                AdditiveTypeId = productIndependentTemplateAdditive.AdditiveTypeId,
                                Amount = productIndependentTemplateAdditive.Amount,
                                IsProductDependent = productIndependentTemplateAdditive.IsProductDependent
                            });
                        });

                        dependentProductSetupAdditives.Add(new Domain.Entities.Experiments.ProductSetupAdditive
                        {
                            AdditiveTypeId = productDependentTemplateAdditive.AdditiveTypeId,
                            Amount = productDependentTemplateAdditive.Amount,
                            IsProductDependent = productDependentTemplateAdditive.IsProductDependent
                        });

                        productSetups.Add(new Domain.Entities.Experiments.ProductSetup
                        {
                            WaterLevel = experimentCandidate.TemplateSetting.WaterLevel,
                            WaterHardness = experimentCandidate.TemplateSetting.WaterHardness,
                            ChlorLevel = experimentCandidate.TemplateSetting.ChlorLevel,
                            MixingRatio = experimentCandidate.TemplateSetting.MixingRatio,
                            RotationTime = experimentCandidate.TemplateSetting.RotationTime,
                            Drumspeed = experimentCandidate.TemplateSetting.Drumspeed,
                            Position = position,
                            ProductId = product.Id,
                            Additives = [.. dependentProductSetupAdditives]
                        });

                        position++;
                    });
                }
            });
        }

        experimentCandidate.TemplateAdditives.Clear();
        experimentCandidate.TemplateSetting = null;

        if (request.CreateExperimentDto.ExperimentIdForProductsOrder.HasValue)
        {
            var experimentToCopyProductOrders = await unitOfWork.Experiments.FindOneAsync(e => e.Id == request.CreateExperimentDto.ExperimentIdForProductsOrder.Value,
                                                                                          e => e.Include(e => e.ProductSetups),
                                                                                          cancellationToken: cancellationToken);

            var productSetupsToCopy = experimentToCopyProductOrders.ProductSetups
                .OrderBy(ps => ps.Position)
                .ToList();

            if (productSetupsToCopy.Count == productSetups.Count)
            {
                foreach (var productSetup in productSetups)
                {
                    var correspondingProductSetup = productSetupsToCopy.FirstOrDefault(ps => ps.ProductId == productSetup.ProductId);

                    if (!Guard.Against.IsNull(correspondingProductSetup))
                        productSetup.Position = correspondingProductSetup.Position;
                }
            }
        }

        experimentCandidate.ProductSetups = [.. productSetups];
        experimentCandidate.StatusOpenDate = dateTimeProvider.Now;

        await unitOfWork.Experiments.InsertOneAsync(experimentCandidate, cancellationToken);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (result < 1)
            return Errors.Infrastructure.CreationError("Experiment");

        var getExperimentByIdQueryResponse = await sender.Send(new GetExperimentByIdQuery(experimentCandidate.Id), cancellationToken);
        return new CreateExperimentCommandResponse(getExperimentByIdQueryResponse.Value.Experiment);
    }
}
