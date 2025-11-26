using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Monitors.SubstrateFamily;
using Hcb.Rnd.Pwn.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hcb.Rnd.Pwn.Common.Extensions;

public static class EnumExtensions
{
    public static bool IsFabricType(this SubstrateFamilyDto substrateFamily) => substrateFamily.SubstrateCategory.Id == (long)SubstrateCategory.Fabric;
    public static bool IsStainType(this SubstrateFamilyDto substrateFamily) => substrateFamily.SubstrateCategory.Id == (long)SubstrateCategory.Stain;
    public static bool IsDelete(this EditorMode editorMode) => editorMode == EditorMode.Delete;
    public static bool IsCreate(this EditorMode editorMode) => editorMode == EditorMode.Create;
    public static bool IsUpdate(this EditorMode editorMode) => editorMode == EditorMode.Update;
    public static bool IsView(this EditorMode editorMode) => editorMode == EditorMode.View;
    public static bool IsInfo(this EditorMode editorMode) => editorMode == EditorMode.Info;

    public static bool IsStatusOpen(this long statusId) => statusId == (long)StatusExperiment.Open;
    public static bool IsStatusValidation(this long statusId) => statusId == (long)StatusExperiment.Validation;
    public static bool IsStatusPreparation(this long statusId) => statusId == (long)StatusExperiment.Preparation;
    public static bool IsStatusMeasurement(this long statusId) => statusId == (long)StatusExperiment.Measurement;
    public static bool IsStatusCompleted(this long statusId) => statusId == (long)StatusExperiment.Completed;

    public static bool IsStatusOpen(this StatusExperiment status) => status == StatusExperiment.Open;
    public static bool IsStatusValidation(this StatusExperiment status) => status == StatusExperiment.Validation;
    public static bool IsStatusPreparation(this StatusExperiment status) => status == StatusExperiment.Preparation;
    public static bool IsStatusMeasurement(this StatusExperiment status) => status == StatusExperiment.Measurement;
    public static bool IsStatusCompleted(this StatusExperiment status) => status == StatusExperiment.Completed;

    public static bool IsMach5(this DeviceType deviceType) => deviceType == DeviceType.Mach5;
    public static bool IsDataColor(this DeviceType deviceType) => deviceType == DeviceType.DataColor;
    public static bool IsVideometer(this DeviceType deviceType) => deviceType == DeviceType.Videometer;
    public static bool IsUSRobot(this DeviceType deviceType) => deviceType == DeviceType.USRobot;
    public static bool IsExternalMach5Upload(this DeviceType deviceType) => deviceType == DeviceType.ExternalMach5Upload;
    public static bool IsExternalExcelUpload(this DeviceType deviceType) => deviceType == DeviceType.ExternalExcelUpload;

    public static bool IsMach5(this long deviceTypeId) => deviceTypeId == (long)DeviceType.Mach5;
    public static bool IsDataColor(this long deviceTypeId) => deviceTypeId == (long)DeviceType.DataColor;
    public static bool IsVideometer(this long deviceTypeId) => deviceTypeId == (long)DeviceType.Videometer;
    public static bool IsUSRobot(this long deviceTypeId) => deviceTypeId == (long)DeviceType.USRobot;
    public static bool IsExternalMach5Upload(this long deviceTypeId) => deviceTypeId == (long)DeviceType.ExternalMach5Upload;
    public static bool IsExternalExcelUpload(this long deviceTypeId) => deviceTypeId == (long)DeviceType.ExternalExcelUpload;

    public static StatusExperiment GetExperimentStatus(this long statusId)
    {
        return statusId switch
        {
            1 => StatusExperiment.Open,
            4 => StatusExperiment.Validation,
            8 => StatusExperiment.Preparation,
            16 => StatusExperiment.Measurement,
            32 => StatusExperiment.Completed,
            _ => throw new NotImplementedException(),
        };
    }

    public static string GetDisplayValue(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (Guard.Against.IsNull(field))
            return value.ToString();

        var attribute = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
        return attribute?.Name ?? value.ToString();
    }

    public static bool IsProductDeveloperView(this ExperimentViewType experimentViewType) => experimentViewType == ExperimentViewType.ProductDeveloper;
    public static bool IsValidatorView(this ExperimentViewType experimentViewType) => experimentViewType == ExperimentViewType.Validator;
    public static bool IsOperatorView(this ExperimentViewType experimentViewType) => experimentViewType == ExperimentViewType.Operator;

    public static bool IsStainRemoval(this ExperimentType experimentType) => experimentType == ExperimentType.StainRemoval;
    public static bool IsAddOnStainRemovalWhiteFabrics(this ExperimentType experimentType) => experimentType == ExperimentType.AddOnStainRemovalWhiteFabrics;
    public static bool IsGrayingInhibitionStressTest(this ExperimentType experimentType) => experimentType == ExperimentType.GrayingInhibitionStressTest;
    public static bool IsSecondaryWhitenessTest(this ExperimentType experimentType) => experimentType == ExperimentType.SecondaryWhitenessTest;
    public static bool IsDyeTransferInhibitionLinitest(this ExperimentType experimentType) => experimentType == ExperimentType.DyeTransferInhibitionLinitest;
    public static bool IsLongtermMultiCycleTesting(this ExperimentType experimentType) => experimentType == ExperimentType.LongtermMultiCycleTesting;
    public static bool IsWashWearTest(this ExperimentType experimentType) => experimentType == ExperimentType.WashWearTest;
    public static bool IsPillingTest(this ExperimentType experimentType) => experimentType == ExperimentType.PillingTest;
    public static bool IsSniffTest(this ExperimentType experimentType) => experimentType == ExperimentType.SniffTest;

    public static bool IsStainRemoval(this long experimentTypeId) => experimentTypeId == (long)ExperimentType.StainRemoval;
    public static bool IsAddOnStainRemovalWhiteFabrics(this long experimentTypeId) => experimentTypeId == (long)ExperimentType.AddOnStainRemovalWhiteFabrics;
    public static bool IsGrayingInhibitionStressTest(this long experimentTypeId) => experimentTypeId == (long)ExperimentType.GrayingInhibitionStressTest;
    public static bool IsSecondaryWhitenessTest(this long experimentTypeId) => experimentTypeId == (long)ExperimentType.SecondaryWhitenessTest;
    public static bool IsDyeTransferInhibitionLinitest(this long experimentTypeId) => experimentTypeId == (long)ExperimentType.DyeTransferInhibitionLinitest;
    public static bool IsLongtermMultiCycleTesting(this long experimentTypeId) => experimentTypeId == (long)ExperimentType.LongtermMultiCycleTesting;
    public static bool IsWashWearTest(this long experimentTypeId) => experimentTypeId == (long)ExperimentType.WashWearTest;
    public static bool IsPillingTest(this long experimentTypeId) => experimentTypeId == (long)ExperimentType.PillingTest;
    public static bool IsSniffTest(this long experimentTypeId) => experimentTypeId == (long)ExperimentType.SniffTest;
}
