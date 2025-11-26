using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Base;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Monitors;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Base;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Monitors;
using Microsoft.Extensions.DependencyInjection;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.AzureGroups;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.AzureGroups;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Experiments;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Experiments;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Orders;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Orders;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Products;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Products;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Additives;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Additives;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Measurements;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Measurements;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Soar;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Soar;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.MeasurementDevices;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.MeasurementDevices;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.StorageAccount;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.StorageAccount;
using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Dashboard;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Dashboard;

namespace Hcb.Rnd.Pwn.Infrastructure.DI;

public static class RepositoriesDependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {

        //Repositories: please keep the order according to the solution explorer

        //Additives
        services.AddScoped<IAdditiveTypeRepository, AdditiveTypeRepository>();

        //AzureGroups
        services.AddScoped<IProductDeveloperAadGroupRepository, ProductDeveloperAadGroupRepository>();
        services.AddScoped<IOperatorAadGroupRepository, OperatorAadGroupRepository>();
        services.AddScoped<IValidatorAadGroupRepository, ValidatorAadGroupRepository>();
        services.AddScoped<IInstrumentManagerAadGroupRepository, InstrumentManagerAadGroupRepository>();

        //Dashboard
        services.AddScoped<IRecentlyViewedExperimentRepository, RecentlyViewedExperimentRepository>();
        services.AddScoped<IRecentlyViewedExperimentTemplateRepository, RecentlyViewedExperimentTemplateRepository>();
        services.AddScoped<IRecentlyViewedMonitorRepository, RecentlyViewedMonitorRepository>();

        //Experiments
        services.AddScoped<ITemplateAdditiveRepository, TemplateAdditiveRepository>();
        services.AddScoped<IExperimentRepository, ExperimentRepository>();
        services.AddScoped<IProductSetupAdditiveRepository, ProductSetupAdditiveRepository>();
        services.AddScoped<IExperimentStatusRepository, ExperimentStatusRepository>();
        services.AddScoped<IExperimentTypeRepository, ExperimentTypeRepository>();
        services.AddScoped<IMonitorSetupRepository, MonitorSetupRepository>();
        services.AddScoped<IProductSetupRepository, ProductSetupRepository>();
        services.AddScoped<IWashCycleSetupRepository, WashCycleSetupRepository>();
        services.AddScoped<IWashingMachineRepository, WashingMachineRepository>();
        services.AddScoped<ITemplateSettingRepository, TemplateSettingRepository>();
        services.AddScoped<IAssignedExperimentTemplateGroupRepository, AssignedExperimentTemplateGroupRepository>();
        services.AddScoped<ILabWashingMachineRepository, LabWashingMachineRepository>();
        services.AddScoped<ILabWashingMachineMappingRepository, LabWashingMachineMappingRepository>();


        //MeasurementDevices
        services.AddScoped<IDataColorProfileRepository, DataColorProfileRepository>();
        services.AddScoped<IMeasurementDeviceRepository, MeasurementDeviceRepository>();
        services.AddScoped<IWhiteTileProfileRepository, WhiteTileProfileRepository>();
        services.AddScoped<IWhiteTileResultRepository, WhiteTileResultRepository>();


        //Measurements
        services.AddScoped<IColorMetricRepository, ColorMetricRepository>();
        services.AddScoped<IDeltaColorMetricRepository, DeltaColorMetricRepository>();
        services.AddScoped<IFrameMonitorRepository, FrameMonitorRepository>();
        services.AddScoped<IFrameSubstrateRepository, FrameSubstrateRepository>();
        services.AddScoped<IPillingMetricRepository, PillingMetricRepository>();
        services.AddScoped<IRadiationWavelengthRepository, RadiationWavelengthRepository>();
        services.AddScoped<IUvColorMetricRepository, UvColorMetricRepository>();
        services.AddScoped<ISubstrateDefaultColorMetricRepository, SubstrateDefaultColorMetricRepository>();
        services.AddScoped<IBatchFrameMonitorRepository, BatchFrameMonitorRepository>();
        services.AddScoped<IBatchFrameSubstrateRepository, BatchFrameSubstrateRepository>();
        services.AddScoped<IOrdinateRepository, OrdinateRepository>();

        //Monitors
        services.AddScoped<IClusterTypeRepository, ClusterTypeRepository>();
        services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();
        services.AddScoped<IFabricTypeRepository, FabricTypeRepository>();
        services.AddScoped<IMonitorRepository, MonitorRepository>();
        services.AddScoped<IMonitorSubstrateRepository, MonitorSubstrateRepository>();
        services.AddScoped<IMonitorTypeRepository, MonitorTypeRepository>();
        services.AddScoped<IProductionModeRepository, ProductionModeRepository>();
        services.AddScoped<IProtocolRepository, ProtocolRepository>();
        services.AddScoped<ISubstrateCategoryRepository, SubstrateCategoryRepository>();
        services.AddScoped<ISubstrateProtocolRepository, SubstrateProtocolRepository>();
        services.AddScoped<ISubstrateFamilyRepository, SubstrateFamilyRepository>();
        services.AddScoped<ISubstrateRepository, SubstrateRepository>();
        services.AddScoped<IFabricCompositionRepository, FabricCompositionRepository>();
        services.AddScoped<IFabricConstructionRepository, FabricConstructionRepository>();
        services.AddScoped<ISubstrateFamilyClusterTypeRepository, SubstrateFamilyClusterTypeRepository>();
        services.AddScoped<ISubstrateFabricCompositionRepository, SubstrateFabricCompositionRepository>();
        services.AddScoped<IMonitorBatchRepository, MonitorBatchRepository>();


        //Orders
        services.AddScoped<IOrderRepository, OrderRepository>();

        //Products
        services.AddScoped<IProductionTypeRepository, ProductionTypeRepository>();
        services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        //Soar
        services.AddScoped<IProductionStatusRepository, ProductionStatusRepository>();
        services.AddScoped<ISoarProductionOrderRepository, SoarProductionOrderRepository>();

        //StorageAccount
        services.AddScoped<IStorageAccountTokenRepository, StorageAccountTokenRepository>();


        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();


        return services;
    }
}
