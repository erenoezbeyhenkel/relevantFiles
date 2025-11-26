using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
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

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.Base;

/// <summary>
/// Created by EO. https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
/// 
/// To handle db operations in one transaction, we use Unit of Work. Add your repository to IUnitOfWork, then implement it here.
/// </summary>
public partial class UnitOfWork(DataBaseContext context,
                                IAdditiveTypeRepository additives,
                                IProductDeveloperAadGroupRepository productDeveloperAadGroups,
                                ITemplateAdditiveRepository templateAdditives,
                                IExperimentRepository experiments,
                                IExperimentStatusRepository experimentStatuses,
                                IExperimentTypeRepository experimentTypes,
                                IMonitorSetupRepository monitorSetups,
                                IProductSetupRepository productSetups,
                                IWashCycleSetupRepository washCycleSetups,
                                IWashingMachineRepository washingMachines,
                                IColorMetricRepository colorMetrics,
                                IDeltaColorMetricRepository deltaColorMetrics,
                                IFrameMonitorRepository frameMonitors,
                                IFrameSubstrateRepository frameSubstrates,
                                IRadiationWavelengthRepository radiationWavelengths,
                                IUvColorMetricRepository uvColorMetrics,
                                IClusterTypeRepository clusterTypes,
                                IDeviceTypeRepository deviceTypes,
                                IFabricTypeRepository fabricTypes,
                                IMonitorRepository monitors,
                                IMonitorSubstrateRepository monitorSubstrates,
                                IMonitorTypeRepository monitorTypes,
                                IProductionModeRepository productionModes,
                                IProtocolRepository protocols,
                                ISubstrateCategoryRepository substrateCategories,
                                ISubstrateProtocolRepository substrateProtocols,
                                ISubstrateFamilyRepository substrateFamilies,
                                ISubstrateRepository substrates,
                                IOrderRepository orders,
                                IProductionTypeRepository productionTypes,
                                IProductRepository products,
                                IProductTypeRepository productTypes,
                                IProductionStatusRepository productionStatuses,
                                IRecentlyViewedExperimentRepository recentlyViewedExperiments,
                                IRecentlyViewedExperimentTemplateRepository recentlyViewedExperimentTemplates,
                                IRecentlyViewedMonitorRepository recentlyViewedMonitors,
                                ISoarProductionOrderRepository soarProductionOrders,
                                IAdditiveTypeRepository additiveTypes,
                                IProductSetupAdditiveRepository productSetupAdditives,
                                ITemplateSettingRepository templateSettings,
                                IDataColorProfileRepository dataColorProfiles,
                                IMeasurementDeviceRepository measurementDevices,
                                IWhiteTileProfileRepository whiteTileProfiles,
                                IWhiteTileResultRepository whiteTileResults,
                                IStorageAccountTokenRepository storageAccountTokens,
                                IFabricCompositionRepository fabricCompositions,
                                IFabricConstructionRepository fabricConstructions,
                                ISubstrateFabricCompositionRepository substrateFabricCompositions,
                                ISubstrateFamilyClusterTypeRepository substrateFamilyClusterTypes,
                                IAssignedExperimentTemplateGroupRepository assignedExperimentTemplateGroups,
                                IValidatorAadGroupRepository validatorAadGroups,
                                IOperatorAadGroupRepository operatorAadGroups,
                                ILabWashingMachineRepository labWashingMachines,
                                ILabWashingMachineMappingRepository labWashingMachineMappings,
                                IInstrumentManagerAadGroupRepository instrumentManagerAadGroups,
                                IPillingMetricRepository pillingMetrics,
                                ISubstrateDefaultColorMetricRepository substrateDefaultColorMetrics,
                                IBatchFrameMonitorRepository batchFrameMonitors,
                                IBatchFrameSubstrateRepository batchFrameSubstrates,
                                IMonitorBatchRepository monitorBatches,
                                IOrdinateRepository ordinates) : IUnitOfWork
{
    public DataBaseContext DbContext = context;

    //Additives
    public IAdditiveTypeRepository Additives => additives;
    public IAdditiveTypeRepository AdditiveTypes => additiveTypes;

    //AzureGroups
    public IProductDeveloperAadGroupRepository ProductDeveloperAadGroups => productDeveloperAadGroups;
    public IValidatorAadGroupRepository ValidatorAadGroups => validatorAadGroups;
    public IOperatorAadGroupRepository OperatorAadGroups => operatorAadGroups;
    public IInstrumentManagerAadGroupRepository InstrumentManagerAadGroups => instrumentManagerAadGroups;

    //Dashboard
    public IRecentlyViewedExperimentRepository RecentlyViewedExperiments => recentlyViewedExperiments;
    public IRecentlyViewedExperimentTemplateRepository RecentlyViewedExperimentTemplates => recentlyViewedExperimentTemplates;
    public IRecentlyViewedMonitorRepository RecentlyViewedMonitors => recentlyViewedMonitors;

    //Experiments
    public ITemplateAdditiveRepository TemplateAdditives => templateAdditives;
    public IExperimentRepository Experiments => experiments;
    public IExperimentStatusRepository ExperimentStatuses => experimentStatuses;
    public IExperimentTypeRepository ExperimentTypes => experimentTypes;
    public IMonitorSetupRepository MonitorSetups => monitorSetups;
    public IProductSetupRepository ProductSetups => productSetups;
    public IProductSetupAdditiveRepository ProductSetupAdditives => productSetupAdditives;
    public IWashCycleSetupRepository WashCycleSetups => washCycleSetups;
    public IWashingMachineRepository WashingMachines => washingMachines;
    public ITemplateSettingRepository TemplateSettings => templateSettings;
    public IAssignedExperimentTemplateGroupRepository AssignedExperimentTemplateGroups => assignedExperimentTemplateGroups;
    public ILabWashingMachineRepository LabWashingMachines => labWashingMachines;
    public ILabWashingMachineMappingRepository LabWashingMachineMappings => labWashingMachineMappings;
    //Measurements
    public IColorMetricRepository ColorMetrics => colorMetrics;
    public IDeltaColorMetricRepository DeltaColorMetrics => deltaColorMetrics;
    public IFrameMonitorRepository FrameMonitors => frameMonitors;
    public IFrameSubstrateRepository FrameSubstrates => frameSubstrates;
    public IRadiationWavelengthRepository RadiationWavelengths => radiationWavelengths;
    public IUvColorMetricRepository UvColorMetrics => uvColorMetrics;
    public IPillingMetricRepository PillingMetrics => pillingMetrics;

    public ISubstrateDefaultColorMetricRepository SubstrateDefaultColorMetrics => substrateDefaultColorMetrics;
    public IBatchFrameMonitorRepository BatchFrameMonitors => batchFrameMonitors;
    public IBatchFrameSubstrateRepository BatchFrameSubstrates => batchFrameSubstrates;
    public IOrdinateRepository Ordinates => ordinates;

    //Monitors
    public IClusterTypeRepository ClusterTypes => clusterTypes;
    public IDeviceTypeRepository DeviceTypes => deviceTypes;
    public IFabricTypeRepository FabricTypes => fabricTypes;
    public IMonitorRepository Monitors => monitors;
    public IMonitorSubstrateRepository MonitorSubstrates => monitorSubstrates;
    public IMonitorTypeRepository MonitorTypes => monitorTypes;
    public IProductionModeRepository ProductionModes => productionModes;
    public IProtocolRepository Protocols => protocols;
    public ISubstrateCategoryRepository SubstrateCategories => substrateCategories;
    public ISubstrateProtocolRepository SubstrateProtocols => substrateProtocols;
    public ISubstrateFamilyRepository SubstrateFamilies => substrateFamilies;
    public ISubstrateRepository Substrates => substrates;
    public IFabricCompositionRepository FabricCompositions => fabricCompositions;
    public IFabricConstructionRepository FabricConstructions => fabricConstructions;
    public ISubstrateFabricCompositionRepository SubstrateFabricCompositions => substrateFabricCompositions;
    public ISubstrateFamilyClusterTypeRepository SubstrateFamilyClusterTypes => substrateFamilyClusterTypes;
    public IMonitorBatchRepository MonitorBatches => monitorBatches;

    //Orders
    public IOrderRepository Orders => orders;

    //Products
    public IProductionTypeRepository ProductionTypes => productionTypes;
    public IProductRepository Products => products;
    public IProductTypeRepository ProductTypes => productTypes;

    //Soar
    public IProductionStatusRepository ProductionStatuses => productionStatuses;
    public ISoarProductionOrderRepository SoarProductionOrders => soarProductionOrders;

    //Devices

    public IDataColorProfileRepository DataColorProfiles => dataColorProfiles;
    public IMeasurementDeviceRepository MeasurementDevices => measurementDevices;
    public IWhiteTileProfileRepository WhiteTileProfiles => whiteTileProfiles;
    public IWhiteTileResultRepository WhiteTileResults => whiteTileResults;

    //StorageAccount
    public IStorageAccountTokenRepository StorageAccountTokens => storageAccountTokens;


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await DbContext.SaveChangesAsync(cancellationToken);

    private bool _disposed;


    /// <summary>
    /// Cleans up any resources being used.
    /// </summary>
    /// <returns><see cref="ValueTask"/></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);

        // Take this object off the finalization queue to prevent 
        // finalization code for this object from executing a second time.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Cleans up any resources being used.
    /// </summary>
    /// <param name="disposing">Whether or not we are disposing</param> 
    /// <returns><see cref="ValueTask"/></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources.
                await DbContext.DisposeAsync();
            }

            // Dispose any unmanaged resources here...

            _disposed = true;
        }
    }
}