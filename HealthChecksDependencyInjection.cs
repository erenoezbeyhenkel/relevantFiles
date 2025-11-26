using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Hcb.Rnd.Pwn.Infrastructure.DI;

public static class HealthChecksDependencyInjection
{
    public static IServiceCollection AddSystemHealthChecks(this IServiceCollection services, DatabaseOptions databaseOptions)
    {

        services.AddHealthChecks()
            .AddSqlServer(connectionString: databaseOptions.AzureSqlServerConnectionString,
            name: "Azure Sql server connection.",
            failureStatus: HealthStatus.Unhealthy,
            tags: ["azure", "db", "sql", "sqlserver"])
            .AddSqlServer(connectionString: databaseOptions.OnPremSqlServerConnectionString,
            name: "Onprem sql server connection.",
            failureStatus: HealthStatus.Unhealthy,
            tags: ["onprem", "db", "sql", "sqlserver"]
            ).AddApplicationInsightsPublisher();


        return services;
    }
}
