using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.WashCycleSetUp;
using Hcb.Rnd.Pwn.Common.Enums;

namespace Hcb.Rnd.Pwn.Common.Extensions;

public static class ExperimentExtensions
{
    public static DateTime? GetExperimentCurrentStatusDate(this ExperimentSummaryDto experimentSummaryDto)
    {
        if (Guard.Against.IsNull(experimentSummaryDto))
            return default;

        return experimentSummaryDto.StatusCompletedDate
            ?? experimentSummaryDto.StatusMeasurementDate
            ?? experimentSummaryDto.StatusPreparationDate
            ?? experimentSummaryDto.StatusValidationDate
            ?? experimentSummaryDto.StatusOpenDate;
    }

    public static DateTime? GetExperimentCurrentStatusDate(this ExperimentDto experimentDto)
    {
        if (Guard.Against.IsNull(experimentDto))
            return default;

        return experimentDto.StatusCompletedDate
            ?? experimentDto.StatusMeasurementDate
            ?? experimentDto.StatusPreparationDate
            ?? experimentDto.StatusValidationDate
            ?? experimentDto.StatusOpenDate;
    }

    public static EditorMode GetEditorMode(this ExperimentSummaryDto experimentSummaryDto, bool isValidation, bool isOperator = false)
    {
        if (isOperator)
            return EditorMode.View;

        if (isValidation && !experimentSummaryDto.ExperimentStatusId.IsStatusValidation())
            return EditorMode.View;

        if (!isValidation && !experimentSummaryDto.ExperimentStatusId.IsStatusOpen())
            return EditorMode.View;

        return EditorMode.Create;
    }

    public static EditorMode GetEditorMode(this long experimentStatusId, bool isValidation, bool isOperator = false)
    {
        if (isOperator)
            return EditorMode.View;

        if (isValidation && !experimentStatusId.IsStatusValidation())
            return EditorMode.View;

        if (!isValidation && !experimentStatusId.IsStatusOpen())
            return EditorMode.View;

        return EditorMode.Create;
    }

    public static string GetFormattedPosition(this int position)
    {
        if (position <= 26)
        {
            return Convert.ToChar(position + 64).ToString();
        }
        int div = position / 26;
        int mod = position % 26;
        if (mod == 0) { mod = 26; div--; }
        return GetFormattedPosition(div) + GetFormattedPosition(mod);
    }

    public static bool AreDefaultValuesUsed(this WashCycleSetupDto washCycleSetupDto) => washCycleSetupDto.IsInitial && washCycleSetupDto.WashCycle == 0 && !Guard.Against.IsNull(washCycleSetupDto.MonitorBatchId);
}
