using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.Extensions;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Common;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Email.Experiment;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.ChangeStatus;
using Hcb.Rnd.Pwn.Common.Enums;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Experiments.Experiment.ChangeStatus;

public sealed class ChangeStatusExperimentCommandHandler(IUnitOfWork unitOfWork,
                                                         IDateTimeProvider dateTimeProvider,
                                                         IExperimentEmailService experimentEmailService) : ICommandHandler<ChangeStatusExperimentCommand, ChangeStatusExperimentCommandResponse>
{
    public async Task<ErrorOr<ChangeStatusExperimentCommandResponse>> Handle(ChangeStatusExperimentCommand request, CancellationToken cancellationToken)
    {
        StatusExperiment newStatus = request.ExperimentStatusId.GetExperimentStatus();

        var existingExperiment = await unitOfWork.Experiments.FindOneAsync(e => e.Id == request.Id && e.OrderId == request.OrderId, e =>
                                              e.Include(exp => exp.MonitorSetups).ThenInclude(monitorSetUp => monitorSetUp.WashCycleSetups)
                                               .Include(exp => exp.MonitorSetups).ThenInclude(monitorSetUp => monitorSetUp.WashCycleSetups)
                                               .Include(exp => exp.MonitorSetups).ThenInclude(monitorSetUp => monitorSetUp.Monitor).ThenInclude(monitor => monitor.MonitorSubstrates)
                                               .Include(experiment => experiment.ProductSetups)
                                               .Include(experiment => experiment.Order)
                                         , cancellationToken: cancellationToken);

        if (Guard.Against.IsNull(existingExperiment))
            return Errors.Infrastructure.DoesNotExist("Experiment");

        StatusExperiment currentStatus = existingExperiment.ExperimentStatusId.GetExperimentStatus();

        var dateNow = dateTimeProvider.Now;

        //Forward
        if (currentStatus.IsStatusOpen() && newStatus.IsStatusValidation())
        {
            existingExperiment.StatusValidationDate = dateNow;

            //Send email
            if (!Guard.Against.IsNull(existingExperiment.ValidatorAadGroupId))
            {
                var validatorAadGroup = await unitOfWork.ValidatorAadGroups.FindOneAsync(vaad => vaad.Id == existingExperiment.ValidatorAadGroupId, cancellationToken: cancellationToken);
                await experimentEmailService.SendExperimentStatusChangingOpenToValidationEmailAsync(validatorAadGroup.Oid.ToString(), validatorAadGroup.Name, existingExperiment, cancellationToken);
            }
        }
        else if (currentStatus.IsStatusValidation() && newStatus.IsStatusPreparation())
        {
            #region Create Frame items logic   

            var frameMonitorCandidateCollection = new List<Domain.Entities.Measurements.FrameMonitor>();

            foreach (var monitorSetup in existingExperiment.MonitorSetups.ToList())
            {
                foreach (var washCycleSetup in monitorSetup.WashCycleSetups.OrderBy(wc => wc.WashCycle))
                {
                    if (washCycleSetup.ToDto().AreDefaultValuesUsed())
                        continue;

                    foreach (var productSetup in existingExperiment.ProductSetups.OrderBy(ps => ps.Position))
                    {
                        for (int sampleCounter = 1; sampleCounter <= monitorSetup.NumberOfSamples; sampleCounter++)
                        {
                            for (int repetitionCounter = 1; repetitionCounter <= existingExperiment.NumberOfRepetition; repetitionCounter++)
                            {
                                var frameMonitorCandidate = new Domain.Entities.Measurements.FrameMonitor
                                {
                                    ProductSetupId = productSetup.Id,
                                    WashCycleSetupId = washCycleSetup.Id,
                                    RepetitionNumber = repetitionCounter,
                                    SampleNumber = sampleCounter,
                                    WashRun = $"{existingExperiment.Id}-{productSetup.Id}-{washCycleSetup.WashCycle}-{productSetup.Position}-{repetitionCounter}",
                                    IsNotAvailable = false,
                                    AzureBlogStorageContainerName = existingExperiment.GetAzureBlogStorageContainerName(monitorSetup, washCycleSetup)
                                };

                                var frameSubstrateCandidateCollection = new List<Domain.Entities.Measurements.FrameSubstrate>();

                                foreach (var mSubstrate in monitorSetup.Monitor.MonitorSubstrates.OrderBy(s => s.Position))
                                {
                                    frameSubstrateCandidateCollection.Add(new Domain.Entities.Measurements.FrameSubstrate()
                                    {
                                        FrameMonitor = frameMonitorCandidate,
                                        SubstrateId = mSubstrate.SubstrateId,
                                    });

                                }
                                frameMonitorCandidate.FrameSubstrates = frameSubstrateCandidateCollection;
                                frameMonitorCandidateCollection.Add(frameMonitorCandidate);
                            }
                        }
                    }
                }
            }

            await unitOfWork.FrameMonitors.InsertManyAsync(frameMonitorCandidateCollection, cancellationToken);
            #endregion


            existingExperiment.StatusPreparationDate = dateNow;

            //Send preparation email
            if (!Guard.Against.IsNull(existingExperiment.OperatorAadGroupId))
            {
                var operatorAadGroup = await unitOfWork.OperatorAadGroups.FindOneAsync(vaad => vaad.Id == existingExperiment.OperatorAadGroupId, cancellationToken: cancellationToken);
                var productDeveloperAadGroup = await unitOfWork.ProductDeveloperAadGroups.FindOneAsync(vaad => vaad.Id == existingExperiment.Order.ProductDeveloperAadGroupId, cancellationToken: cancellationToken);
                await experimentEmailService.SendExperimentStatusChangingValidationToPreparationTemplateEmailAsync(operatorAadGroup.Oid.ToString(), operatorAadGroup.Name, productDeveloperAadGroup.Oid.ToString(), existingExperiment, cancellationToken);
            }
        }
        else if (currentStatus.IsStatusPreparation() && newStatus.IsStatusMeasurement())
        {
            existingExperiment.StatusMeasurementDate = dateNow;
        }
        else if (currentStatus.IsStatusMeasurement() && newStatus.IsStatusCompleted())
        {
            existingExperiment.StatusCompletedDate = dateNow;

            //Send complete email
            if (!Guard.Against.IsNull(existingExperiment.ValidatorAadGroupId) && !Guard.Against.IsNull(existingExperiment.Order.ProductDeveloperAadGroupId))
            {
                var validatorAadGroup = await unitOfWork.ValidatorAadGroups.FindOneAsync(vaad => vaad.Id == existingExperiment.ValidatorAadGroupId, cancellationToken: cancellationToken);
                var productDeveloperAadGroup = await unitOfWork.ProductDeveloperAadGroups.FindOneAsync(vaad => vaad.Id == existingExperiment.Order.ProductDeveloperAadGroupId, cancellationToken: cancellationToken);
                await experimentEmailService.SendExperimentStatusChangingMeasurementToCompleteTemplateEmailAsync(validatorAadGroup.Oid.ToString(), productDeveloperAadGroup.Oid.ToString(), existingExperiment, cancellationToken);
            }
        }

        //Back
        else if (currentStatus.IsStatusValidation() && newStatus.IsStatusOpen())
        {
            existingExperiment.StatusValidationDate = null;
            existingExperiment.StatusOpenDate = dateNow;
        }
        else if (currentStatus.IsStatusPreparation() && newStatus.IsStatusValidation())
        {
            // Get all WashCycleSetup IDs
            var washCycleSetupIds = existingExperiment.MonitorSetups
                .SelectMany(ms => ms.WashCycleSetups)
                .Select(wcs => wcs.Id)
                .ToList();

            // Delete the retrieved FrameMonitors    
            await unitOfWork.FrameMonitors.DeleteByQueryAsync(fm => washCycleSetupIds.Contains(fm.WashCycleSetupId), cancellationToken: cancellationToken).ConfigureAwait(false);

            existingExperiment.StatusValidationDate = dateNow;
            existingExperiment.StatusPreparationDate = null;
        }
        else if (currentStatus.IsStatusMeasurement() && newStatus.IsStatusPreparation())
        {
            //Actions perhaps needed to prepare statistical evaluation   
            existingExperiment.StatusPreparationDate = dateNow;
            existingExperiment.StatusMeasurementDate = null;
        }
        else if (currentStatus.IsStatusCompleted() && newStatus.IsStatusMeasurement())
        {
            //Actions perhaps needed to prepare statistical evaluation   
            existingExperiment.StatusMeasurementDate = dateNow;
            existingExperiment.StatusCompletedDate = null;
        }
        else
        {
            return Errors.Experiment.InvalidStatusChange("Experiment Status Change is not allow");
        }

        //Workaround for duclicate monitors.
        foreach (var monitorSetup in existingExperiment.MonitorSetups)
            monitorSetup.Monitor = null;

        existingExperiment.ExperimentStatusId = request.ExperimentStatusId;
        await unitOfWork.Experiments.UpdateOneAsync(existingExperiment, cancellationToken);
        var resultEFCore = await unitOfWork.SaveChangesAsync(cancellationToken);
        return new ChangeStatusExperimentCommandResponse(request.Id,
                                                         request.OrderId,
                                                         request.ExperimentStatusId,
                                                         dateNow);
    }
}
