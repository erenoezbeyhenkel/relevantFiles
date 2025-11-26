using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Additives;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.AzureGroups;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Dashboard;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Experiments;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.MeasurementDevices;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Measurements;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Monitors;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Orders;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Products;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Soar;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.StorageAccount;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;

/// <summary>
/// https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
/// </summary>
public interface IUnitOfWork : IAsyncDisposable
{
    //Additives
    IAdditiveTypeRepository Additives { get; }
    IAdditiveTypeRepository AdditiveTypes { get; }

    //AzureGroups
    IProductDeveloperAadGroupRepository ProductDeveloperAadGroups { get; }
    IValidatorAadGroupRepository ValidatorAadGroups { get; }
    IOperatorAadGroupRepository OperatorAadGroups { get; }
    IInstrumentManagerAadGroupRepository InstrumentManagerAadGroups { get; }

    //Dashboard
    IRecentlyViewedExperimentRepository RecentlyViewedExperiments { get; }
    IRecentlyViewedMonitorRepository RecentlyViewedMonitors { get; }
    IRecentlyViewedExperimentTemplateRepository RecentlyViewedExperimentTemplates { get; }

    //Experiments
    ITemplateAdditiveRepository TemplateAdditives { get; }
    IExperimentRepository Experiments { get; }
    IExperimentStatusRepository ExperimentStatuses { get; }
    IExperimentTypeRepository ExperimentTypes { get; }
    IMonitorSetupRepository MonitorSetups { get; }
    IProductSetupRepository ProductSetups { get; }
    IProductSetupAdditiveRepository ProductSetupAdditives { get; }
    IWashCycleSetupRepository WashCycleSetups { get; }
    IWashingMachineRepository WashingMachines { get; }
    ITemplateSettingRepository TemplateSettings { get; }
    IAssignedExperimentTemplateGroupRepository AssignedExperimentTemplateGroups { get; }
    ILabWashingMachineRepository LabWashingMachines { get; }
    ILabWashingMachineMappingRepository LabWashingMachineMappings { get; }

    //Measurements
    IColorMetricRepository ColorMetrics { get; }
    IDeltaColorMetricRepository DeltaColorMetrics { get; }
    IFrameMonitorRepository FrameMonitors { get; }
    IFrameSubstrateRepository FrameSubstrates { get; }
    IRadiationWavelengthRepository RadiationWavelengths { get; }
    IUvColorMetricRepository UvColorMetrics { get; }
    IPillingMetricRepository PillingMetrics { get; }
    ISubstrateDefaultColorMetricRepository SubstrateDefaultColorMetrics { get; }
    IBatchFrameSubstrateRepository BatchFrameSubstrates { get; }
    IBatchFrameMonitorRepository BatchFrameMonitors { get; }
    IOrdinateRepository Ordinates { get; }

    //Monitors
    IClusterTypeRepository ClusterTypes { get; }
    IDeviceTypeRepository DeviceTypes { get; }
    IFabricTypeRepository FabricTypes { get; }
    IMonitorRepository Monitors { get; }
    IMonitorSubstrateRepository MonitorSubstrates { get; }
    IMonitorTypeRepository MonitorTypes { get; }
    IProductionModeRepository ProductionModes { get; }
    IProtocolRepository Protocols { get; }
    ISubstrateCategoryRepository SubstrateCategories { get; }
    ISubstrateProtocolRepository SubstrateProtocols { get; }
    ISubstrateFamilyRepository SubstrateFamilies { get; }
    ISubstrateRepository Substrates { get; }
    IFabricCompositionRepository FabricCompositions { get; }
    IFabricConstructionRepository FabricConstructions { get; }
    ISubstrateFabricCompositionRepository SubstrateFabricCompositions { get; }
    ISubstrateFamilyClusterTypeRepository SubstrateFamilyClusterTypes { get; }
    IMonitorBatchRepository MonitorBatches { get; }

    //Orders
    IOrderRepository Orders { get; }

    //Products
    IProductRepository Products { get; }
    IProductionTypeRepository ProductionTypes { get; }
    IProductTypeRepository ProductTypes { get; }

    //Soar
    IProductionStatusRepository ProductionStatuses { get; }
    ISoarProductionOrderRepository SoarProductionOrders { get; }

    //Devices
    IWhiteTileProfileRepository WhiteTileProfiles { get; }
    IDataColorProfileRepository DataColorProfiles { get; }
    IMeasurementDeviceRepository MeasurementDevices { get; }
    IWhiteTileResultRepository WhiteTileResults { get; }

    //StorageAccount
    IStorageAccountTokenRepository StorageAccountTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
