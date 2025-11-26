using ClosedXML.Excel;
using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Kafka;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Notifications.ExperimentLoadingWashRun;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetWashRunData;
using MediatR;
using Microsoft.Extensions.Options;
using Hcb.Rnd.Pwn.Application.Common.Helpers;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Email.Experiment;

namespace Hcb.Rnd.Pwn.Application.Features.Notifications.ExperimentLoadingWashRun;

public sealed class ExperimentLoadingWashRunNotificationHandler(IKafkaService kafkaService,
                                                                ISender sender,
                                                                IExperimentEmailService experimentEmailService,
                                                                IOptions<KafkaOptions> kafkaOptions) : INotificationHandler<ExperimentLoadingWashRunNotification>
{
    public async Task Handle(ExperimentLoadingWashRunNotification notification, CancellationToken cancellationToken)
    {
        var getWashRunDataQueryResult = await sender.Send(new GetWashRunDataQuery(notification.ExperimentLoadingWashRunNotificationDtos), cancellationToken);
        foreach (var washRunDataDto in getWashRunDataQueryResult.Value?.WashRunDataDtos ?? [])
            await kafkaService.ProduceAsync(kafkaOptions.Value.Topic, washRunDataDto, cancellationToken);

        using var workbook = new XLWorkbook();

        workbook.ExportWashRunDataToExcel(getWashRunDataQueryResult.Value?.WashRunDataDtos ?? []);

        var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        await experimentEmailService.SendExperimentLoadingWashRunNotificationEmail(notification.ExperimentLoadingWashRunNotificationDtos.FirstOrDefault().ExperimentId, Convert.ToBase64String(stream.ToArray()));
    }
}
