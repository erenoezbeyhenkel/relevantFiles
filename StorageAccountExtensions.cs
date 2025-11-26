using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Domain.Entities.Measurements;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions;

public static class StorageAccountExtensions
{
    public static string GetUSRobotMeasurementFileBlobName(this WashCycleSetup washCycleSetup) => $"wcsid{washCycleSetup.Id}-wc{washCycleSetup.WashCycle}-USRobotMeasurementFile.xlsx";
    public static string GetExternalUploadExcelMeasurementFileBlobName(this WashCycleSetup washCycleSetup) => $"wcsid{washCycleSetup.Id}-wc{washCycleSetup.WashCycle}-ExternalUploadExcelMeasurementFile.xlsx";
    public static string GetExperimentOverviewTemplateBlobName() => "template-experiment-overview.xlsx";
    public static string GetUSRobotExcelTemplateName() => "us-robot-excel-template.xlsx";
    public static string GetExternalUploadExcelTemplateName() => "external-upload-excel-template.xlsx";
    public static string GetFrameMonitorImageBlobName(this FrameMonitor frameMonitor) => $"m{frameMonitor.Id}.jpg";
    public static string GetBatchFrameMonitorImageBlobName(this BatchFrameMonitor batchFrameMonitor) => $"m{batchFrameMonitor.Id}.jpg";
    public static string GetFrameSubstrateImageBlobName(this FrameSubstrate frameSubstrate) => $"s{frameSubstrate.Id}.jpg";
    public static string GetBatchFrameSubstrateImageBlobName(this BatchFrameSubstrate batchFrameSubstrate) => $"s{batchFrameSubstrate.Id}.jpg";
    public static string GetPillingMeticImageBlobName(this PillingMetric pillingMetric) => $"p{pillingMetric.Id}.jpg";

}
