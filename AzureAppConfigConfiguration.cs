using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Hcb.Rnd.Pwn.Application.Common.Constants;

namespace Hcb.Rnd.Pwn.Api.DI;

/// <summary>
/// Includes the options for the Azure App Configuration
/// </summary>
public static class AzureAppConfigConfiguration
{
    /// <summary>
    /// To bind the options from the Azure App Configuration
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAzureAppConfigConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(OptionConstants.DatabaseOptions));
        services.Configure<AzureAdOptions>(configuration.GetSection(OptionConstants.AzureAdOptions));
        services.Configure<HugoOptions>(configuration.GetSection(OptionConstants.HugoOptions));
        services.Configure<HdpOptions>(configuration.GetSection(OptionConstants.HdpOptions));
        services.Configure<OrderManagementOptions>(configuration.GetSection(OptionConstants.OrderManagement));
        services.Configure<StorageAccountOptions>(configuration.GetSection(OptionConstants.StorageAccountOptions));
        services.Configure<DevOpsOptions>(configuration.GetSection(OptionConstants.DevOpsOptions));
        services.Configure<SendGridOptions>(configuration.GetSection(OptionConstants.SendGridOptions));
        services.Configure<ApplicationOptions>(configuration.GetSection(OptionConstants.ApplicationOptions));
        services.Configure<DashboardOptions>(configuration.GetSection(OptionConstants.DashboardOptions));
        services.Configure<KafkaOptions>(configuration.GetSection(OptionConstants.KafkaOptions));
        services.Configure<WashAiOptions>(configuration.GetSection(OptionConstants.WashAiOptions));


        return services;
    }
}
