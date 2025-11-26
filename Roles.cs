namespace Hcb.Rnd.Pwn.Common.Constants;

public static class Roles
{
    public const string MonitorManager = "Monitor.Manager";
    public const string MonitorBatchManager = "MonitorBatch.Manager";
    public const string InfrastructureManager = "Infrastructure.Manager";
    public const string HcbpaManager = "HCBPA.Manager";
    public const string DeviceManager = "Device.Manager";
    public const string WashOperator = "Wash.Operator";
    public const string WashManagerValidator = "Wash.Manager.Validator";
    public const string WashManagerProductDeveloper = "Wash.Manager.ProductDeveloper";
    public const string WashManagerTemplateCreator = "Wash.Manager.TemplateCreator";
    public const string InstrumentManager = "Instrument.Manager";

    public const string All = $"{MonitorManager},{InfrastructureManager},{DeviceManager},{HcbpaManager},{WashOperator},{WashManagerValidator},{WashManagerProductDeveloper},{WashManagerTemplateCreator},{InstrumentManager},{MonitorBatchManager}";

    public const string MonitorManagerAndMonitorBatchManager = $"{MonitorManager}, {MonitorBatchManager}";

    public const string WashManagerValidatorAndWashOperator = $"{WashManagerValidator}, {WashOperator}";
    public const string WashManagerValidatorAndProductDeveloper = $"{WashManagerValidator},{WashManagerProductDeveloper}";
    public const string WashManagerValidatorAndTemplateCreator = $"{WashManagerValidator},{WashManagerTemplateCreator}";
    public const string WashManagerProductDeveloperAndTemplateCreator = $"{WashManagerProductDeveloper},{WashManagerTemplateCreator}";
    public const string WashManagerValidatorAndProductDeveloperAndOperator = $"{WashManagerValidator},{WashManagerProductDeveloper},{WashOperator}";

    public const string WashManagerValidatorAndProductDeveloperAndTemplateCreator = $"{WashManagerValidator},{WashManagerProductDeveloper},{WashManagerTemplateCreator}";
    public const string WashManagerValidatorAndProductDeveloperAndTemplateCreatorAndOperator = $"{WashManagerValidator},{WashManagerProductDeveloper},{WashManagerTemplateCreator},{WashOperator}";

    public const string DeviceManagerAndWashOperator = $"{DeviceManager}, {WashOperator}";
    public const string WashOperatorAndProductDeveloper = $"{WashOperator}, {WashManagerProductDeveloper}";
    public const string WashManagerValidatorAndProductDeveloperAndTemplateCreatorAndMonitorManager = $"{WashManagerValidator},{WashManagerProductDeveloper},{WashManagerTemplateCreator}, {MonitorManager}";
    public const string WashManagerValidatorAndProductDeveloperAndTemplateCreatorAndMonitorManagerAndOperator = $"{WashManagerValidator},{WashManagerProductDeveloper},{WashManagerTemplateCreator}, {MonitorManager}, {WashOperator}";
    public const string WashManagerValidatorAndProductDeveloperAndTemplateCreatorAndMonitorManagerAndOperatorAndMonitorBatchManager = $"{WashManagerValidator},{WashManagerProductDeveloper},{WashManagerTemplateCreator}, {MonitorManager}, {WashOperator}, {MonitorBatchManager}";

}
