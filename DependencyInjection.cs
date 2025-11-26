using Azure.Identity;
using Hcb.Rnd.Pwn.Api.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Hcb.Rnd.Pwn.Application.Common.Constants;
using System.Reflection;

namespace Hcb.Rnd.Pwn.Api.DI;

/// <summary>
/// DependencyInjection class of presentation layer
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// includes presentation layer di extensions
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddHealthChecksUI(options =>
        {
            options.DisableDatabaseMigrations();
            options.SetNotifyUnHealthyOneTimeUntilChange();
            options.SetEvaluationTimeInSeconds(86400); // Configures the UI to poll for healthchecks updates once a day.
            options.SetMinimumSecondsBetweenFailureNotifications(60);
            options.SetNotifyUnHealthyOneTimeUntilChange(); // You will only receive one failure notification until the status changes.
        }).AddInMemoryStorage();

        //Azure app configuration set up
        configuration.AddAzureAppConfiguration(options =>
        {
            var uriString = configuration.GetValue<string>("Endpoints:AppConfig");
            options.Connect(new Uri(uriString), new DefaultAzureCredential())
            .Select("Pwn:*", labelFilter: null)
            .ConfigureRefresh(refreshOptions => refreshOptions.Register("Pwn:*", refreshAll: true)
            .SetRefreshInterval(TimeSpan.FromSeconds(3)));
        });

        //Add azure app config to services
        services.AddAzureAppConfiguration();

        //Get the Azure Ad Options from appsetting.json.
        var azureAdOptions = configuration.GetSection(OptionConstants.AzureAdOptions).Get<AzureAdOptions>();

        //Azure key vault setup
        configuration.AddAzureKeyVault(new Uri(azureAdOptions.KeyVaultUrl), new DefaultAzureCredential());

        //Compressing responses to improve performance and reduce the size of the HttpResponse
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
        });

        //Setup azure app config options. This is our custom options
        services.AddAzureAppConfigConfiguration(configuration);

        services.AddControllers();

        //Inject our custom WashNetProblemDetailsFactory as ProblemDetailsFactory
        services.AddSingleton<ProblemDetailsFactory, WashNetProblemDetailsFactory>();
        services.AddEndpointsApiExplorer();

        //Configure Azure identity authentication
        services.AddMicrosoftIdentityWebApiAuthentication(configuration, configSectionName: OptionConstants.AzureAdOptions);

        //https://dev.to/425show/secure-open-api-swagger-calls-with-azure-active-directory-jj7
        //We configure the swagger. Please follow descriptions below.
        services.AddSwaggerGen(options =>
        {
            //Convert the scopes as a dictionary
            var scopes = azureAdOptions.Scopes.ToDictionary(s => s, s => s[(s.LastIndexOf('/') + 1)..]);
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Process);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.SwaggerDoc("v1", new OpenApiInfo { Title = $"Hcb Rnd Pwn - {environment}", Version = $".Net Core - {Environment.Version}" });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = $"Hcb Rnd Pwn - {environment} Token",
                Name = "oauth2",
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        //We set Azure auth url here. It comes from Azure.
                        AuthorizationUrl = new Uri(azureAdOptions.AuthorizationUrl),

                        //We set Azure token url here. It comes from Azure.
                        TokenUrl = new Uri(azureAdOptions.TokenUrl),

                        //Set Azure scopes here to log in.
                        Scopes = scopes
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                    },

                    //Set Azure scopes here to log in.
                    azureAdOptions.Scopes
                }
            });
        });
        return services;
    }
}
