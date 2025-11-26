using Hcb.Rnd.Pwn.Application.Interfaces.Authentication;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Common;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Email.Experiment;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Email;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Kafka;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.MicrosoftGraph;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.StorageAccount;
using Hcb.Rnd.Pwn.Infrastructure.Authentication;
using Hcb.Rnd.Pwn.Infrastructure.Services.Common;
using Hcb.Rnd.Pwn.Infrastructure.Services.Email.Experiment;
using Hcb.Rnd.Pwn.Infrastructure.Services.Email;
using Hcb.Rnd.Pwn.Infrastructure.Services.Kafka;
using Hcb.Rnd.Pwn.Infrastructure.Services.MicrosoftGraph;
using Hcb.Rnd.Pwn.Infrastructure.Services.StorageAccount;
using Microsoft.Extensions.DependencyInjection;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.KeyVault;
using Hcb.Rnd.Pwn.Infrastructure.Services.KeyVault;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Email.BackgroundJob;
using Hcb.Rnd.Pwn.Infrastructure.Services.Email.BackgroundJob;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.DataColor;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.AddOnStainRemovalWhiteFabrics;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.GrayingInhibitionStressTest;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.StainRemoval;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.SecondaryWhitenessTest;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.DyeTransferInhibitionLinitest;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.LongtermMultiCycleTesting;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.EpsBiofilm;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.ColorCare;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.QuickMeasurement;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.DataColor.Uv;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.Uv;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.DeltaColorMetric;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DeltaColorMetric;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.Mach5;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.Mach5;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.UsRobot.StainRemoval;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.Mach5.StainRemoval;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.ExternalMach5Upload.StainRemoval;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.ExternalExcelUpload.StainRemoval;
using Hcb.Rnd.Pwn.Infrastructure.Services.ColorValueCalculation.DataColor.UvGanzCheck;

namespace Hcb.Rnd.Pwn.Infrastructure.DI;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeService>();
        services.AddSingleton<IPwnHttpContextAccessor, PwnHttpContextAccessor>();
        services.AddSingleton<IStorageAccountAccessService, StorageAccountAccessService>();
        services.AddSingleton<IEmailService, EmailService>();
        services.AddSingleton<IMicrosoftGraphService, MicrosoftGraphService>();
        services.AddSingleton<IExperimentEmailService, ExperimentEmailService>();
        services.AddSingleton<IBackgroundJobEmailService, BackgroundJobEmailService>();
        services.AddSingleton<IStorageAccountService, StorageAccountService>();
        services.AddSingleton<IKafkaService, KafkaService>();
        services.AddSingleton<IKeyVaultService, KeyVaultService>();

        //Datacolor calculation
        services.AddScoped<IDataColorUvMeasurementCalculator, DataColorUvMeasurementCalculator>();
        services.AddScoped<IDeltaColorMetricCalculator, DeltaColorMetricCalculator>();
        services.AddScoped<IDataColorCalculator, AddOnStainRemovalWhiteFabricsReferenceScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, AddOnStainRemovalWhiteFabricsWashCycleScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, GrayingInhibitionStressTestReferenceScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, GrayingInhibitionStressTestWashCycleScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, StainRemovalReferenceScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, StainRemovalWashCycleScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, SecondaryWhitenessTestReferenceScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, SecondaryWhitenessTestWashCycleScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, DyeTransferInhibitionLinitestReferenceScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, DyeTransferInhibitionLinitestWashCycleScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, LongtermMultiCycleTestingReferenceScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, LongtermMultiCycleTestingWashCycleScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, EpsBiofilmReferenceScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, EpsBiofilmWashCycleScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, ColorCareReferenceScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, ColorCareWashCycleScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, QuickMeasurementScenarioCalculator>();
        services.AddScoped<IDataColorCalculator, DataColorUvGanzCheckScenarioCalculator>();

        services.AddScoped<IDataColorCalculatorFactory, DataColorCalculatorFactory>();

        //Mach5 calculation
        services.AddScoped<IMach5Calculator, Mach5StainRemovalReferenceScenarioCalculator>();
        services.AddScoped<IMach5Calculator, Mach5StainRemovalWashCycleScenarioCalculator>();

        //Us Robot
        services.AddScoped<IMach5Calculator, UsRobotStainRemovalWashCycleScenarioCalculator>();

        //External Mach5 Upload
        services.AddScoped<IMach5Calculator, ExternalMach5UploadStainRemovalReferenceScenarioCalculator>();
        services.AddScoped<IMach5Calculator, ExternalMach5UploadStainRemovalWashCycleScenarioCalculator>();

        //External Excel Upload
        services.AddScoped<IMach5Calculator, ExternalExcelUploadStainRemovalReferenceScenarioCalculator>();
        services.AddScoped<IMach5Calculator, ExternalExcelUploadStainRemovalWashCycleScenarioCalculator>();

        services.AddScoped<IMach5CalculatorFactory, Mach5CalculatorFactory>();

        return services;
    }
}
