using Hcb.Rnd.Pwn.Infrastructure.Persistence.Connection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Hcb.Rnd.Pwn.Application.Common.Constants;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Base;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SendGrid.Extensions.DependencyInjection;

namespace Hcb.Rnd.Pwn.Infrastructure.DI;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddAppInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddHybridCache();

        var databaseOptions = configuration.GetSection(OptionConstants.DatabaseOptions).Get<DatabaseOptions>();

        //Db context configuration
        services.AddDbContext<DataBaseContext>(dbContextOptionsBuilder =>
        {
            SqlAuthenticationProvider.SetProvider(SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow, new AzureSqlAuthenticationProvider());
            dbContextOptionsBuilder.UseSqlServer(databaseOptions.AzureSqlServerConnectionString, sqlServerAction =>
            {
                //Some options configuration.
                sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                sqlServerAction.CommandTimeout(databaseOptions.CommandTimeOut);

                //To set all queries as SplitQuery
                //sqlServerAction.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            //To ignore pending model changes warnings.
            //https://github.com/dotnet/efcore/issues/34431
            dbContextOptionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnabledDetailedError);
            dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            dbContextOptionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        //Add health cheks here
        services.AddSystemHealthChecks(databaseOptions);

        //Add semd grid
        services.AddSendGrid(options =>
        {
            options.ApiKey = configuration[OptionConstants.SendGridApiKey];
            options.SetDataResidency("eu");
        });

        //Services
        services.AddServices();
        services.AddRepositories();
        services.AddBackgroundJobs();
        services.AddHttpClients(configuration);

        return services;
    }
}
