using Hcb.Rnd.Pwn.Application.Interfaces.Authentication;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Common;
using Hcb.Rnd.Pwn.Domain.Entities.Additives;
using Hcb.Rnd.Pwn.Domain.Entities.AzureGroups;
using Hcb.Rnd.Pwn.Domain.Entities.Dashboard;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Domain.Entities.MeasurementDevices;
using Hcb.Rnd.Pwn.Domain.Entities.Measurements;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;
using Hcb.Rnd.Pwn.Domain.Entities.Orders;
using Hcb.Rnd.Pwn.Domain.Entities.Products;
using Hcb.Rnd.Pwn.Domain.Entities.Soar;
using Hcb.Rnd.Pwn.Domain.Entities.StorageAccount;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Monitor = Hcb.Rnd.Pwn.Domain.Entities.Monitors.Monitor;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.Base;

/// <summary>
/// Our db context.
/// </summary>
/// <param name="dbContextOptions"></param>
/// <param name="pwnHttpContextAccessor"></param>
/// <param name="dateTimeProvider"></param>
public class DataBaseContext(DbContextOptions<DataBaseContext> dbContextOptions,
                             IPwnHttpContextAccessor pwnHttpContextAccessor,
                             IDateTimeProvider dateTimeProvider) : DbContext(dbContextOptions)
{
    public string SchemaName { get; } = "wash";
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configure the schema
        modelBuilder.HasDefaultSchema(SchemaName);

        //This line scans the assembly and finds the Fluent Api entity configuration. Then it configures the entity configurations.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //Model builder goes to base to action.
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Use here to inject interceptors.
        optionsBuilder.AddInterceptors(new UpdateBaseEntityInterceptor(pwnHttpContextAccessor, dateTimeProvider));
        base.OnConfiguring(optionsBuilder);
    }

    //Our entities DbSet. Please implement new entities here. 
    //Use plural naming for the tables.
    //Please keep the order according the solution explorer!

    //Additives
    public virtual DbSet<AdditiveType> AdditiveTypes { get; set; }

    //AzureGroups
    public virtual DbSet<ProductDeveloperAadGroup> ProductDeveloperAadGroups { get; set; }
    public virtual DbSet<OperatorAadGroup> OperatorAadGroups { get; set; }
    public virtual DbSet<ValidatorAadGroup> ValidatorAadGroups { get; set; }
    public virtual DbSet<InstrumentManagerAadGroup> InstrumentManagerAadGroups { get; set; }

    //DashBoard
    public virtual DbSet<RecentlyViewedExperiment> RecentlyViewedExperiments { get; set; }
    public virtual DbSet<RecentlyViewedExperimentTemplate> RecentlyViewedExperimentTemplates { get; set; }
    public virtual DbSet<RecentlyViewedMonitor> RecentlyViewedMonitors { get; set; }

    //Experiments
    public virtual DbSet<TemplateAdditive> TemplateAdditives { get; set; }
    public virtual DbSet<Experiment> Experiments { get; set; }
    public virtual DbSet<AssignedExperimentTemplateGroup> AssignedExperimentTemplateGroups { get; set; }
    public virtual DbSet<ExperimentStatus> ExperimentStatuses { get; set; }
    public virtual DbSet<ExperimentType> ExperimentTypes { get; set; }
    public virtual DbSet<MonitorSetup> MonitorSetups { get; set; }

    public virtual DbSet<ProductSetup> ProductSetups { get; set; }
    public virtual DbSet<ProductSetupAdditive> ProductSetupAdditives { get; set; }
    public virtual DbSet<TemplateSetting> TemplateSettings { get; set; }
    public virtual DbSet<WashCycleSetup> WashCycleSetups { get; set; }
    public virtual DbSet<WashingMachine> WashingMachines { get; set; }
    public virtual DbSet<LabWashingMachine> LabWashingMachines { get; set; }
    public virtual DbSet<LabWashingMachineMapping> LabWashingMachineMappings { get; set; }

    //MeasurementDevices
    public virtual DbSet<MeasurementDevice> MeasurementDevices { get; set; }
    public virtual DbSet<DataColorProfile> DataColorProfiles { get; set; }
    public virtual DbSet<WhiteTileProfile> WhiteTileProfiles { get; set; }
    public virtual DbSet<WhiteTileResult> WhiteTileResults { get; set; }


    //Measurements
    public virtual DbSet<ColorMetric> ColorMetrics { get; set; }
    public virtual DbSet<DeltaColorMetric> DeltaColorMetrics { get; set; }
    public virtual DbSet<FrameMonitor> FrameMonitors { get; set; }
    public virtual DbSet<FrameSubstrate> FrameSubstrates { get; set; }
    public virtual DbSet<PillingMetric> PillingMetrics { get; set; }
    public virtual DbSet<RadiationWavelength> RadiationWavelengths { get; set; }
    public virtual DbSet<UvColorMetric> UvColorMetrics { get; set; }
    public virtual DbSet<BatchFrameMonitor> BatchFrameMonitors { get; set; }
    public virtual DbSet<BatchFrameSubstrate> BatchFrameSubstrates { get; set; }
    public virtual DbSet<SubstrateDefaultColorMetric> SubstrateDefaultColorMetrics { get; set; }
    public virtual DbSet<Ordinate> Ordinates { get; set; }


    //Monitors
    public virtual DbSet<ClusterType> ClusterTypes { get; set; }
    public virtual DbSet<DeviceType> DeviceTypes { get; set; }
    public virtual DbSet<FabricType> FabricTypes { get; set; }
    public virtual DbSet<Monitor> Monitors { get; set; }
    public virtual DbSet<MonitorSubstrate> MonitorSubstrates { get; set; }
    public virtual DbSet<MonitorType> MonitorTypes { get; set; }
    public virtual DbSet<ProductionMode> ProductionModes { get; set; }
    public virtual DbSet<Protocol> Protocols { get; set; }
    public virtual DbSet<Substrate> Substrates { get; set; }
    public virtual DbSet<SubstrateCategory> SubstrateCategories { get; set; }
    public virtual DbSet<SubstrateFamily> SubstrateFamilies { get; set; }
    public virtual DbSet<SubstrateProtocol> SubstrateProtocols { get; set; }

    public virtual DbSet<FabricComposition> FabricCompositions { get; set; }
    public virtual DbSet<FabricConstruction> FabricConstructions { get; set; }
    public virtual DbSet<SubstrateFabricComposition> SubstrateFabricCompositions { get; set; }
    public virtual DbSet<SubstrateFamilyClusterType> SubstrateFamilyClusterTypes { get; set; }

    public virtual DbSet<MonitorBatch> MonitorBatches { get; set; }

    //Orders
    public virtual DbSet<Order> Orders { get; set; }

    //Products
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductionType> ProductionTypes { get; set; }
    public virtual DbSet<ProductType> ProductTypes { get; set; }

    //SOAR
    public virtual DbSet<ProductionStatus> ProductionStatuses { get; set; }
    public virtual DbSet<SoarProductionOrder> SoarProductionOrders { get; set; }

    //StorageAccount
    public virtual DbSet<StorageAccountToken> StorageAccountTokens { get; set; }
}
