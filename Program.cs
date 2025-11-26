using Hcb.Rnd.Pwn.Api.DI;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Hcb.Rnd.Pwn.Application.Common.Constants;
using Hcb.Rnd.Pwn.Application.DI;
using Hcb.Rnd.Pwn.Infrastructure.DI;
using Serilog;
using Serilog.Events;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Base;


try
{
    //Set up logger configuration
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateBootstrapLogger();

    //First log once project runs
    Log.Information("PWN.Api runs.");

    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();

    //Set up Serilog configuration
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    //We inject our project layers in order. Presentation -> Application -> Infrastructure
    builder.Services
        .AddPresentation(builder.Configuration)
        .AddApplication()
        .AddAppInfrastructure(builder.Configuration);

    var policyName = "allowedOrigins";

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: policyName,
            policy =>
            {
                var allowedPolicies = builder.Configuration.GetSection(OptionConstants.PwnAllowedOrigins).Get<string[]>();
                policy.WithOrigins(allowedPolicies)
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
    });

    var app = builder.Build();
    app.UseCors(policyName);

    //Compressing responses to improve performance and reduce the size of the HttpResponse
    app.UseResponseCompression();

    // Migrate latest database changes during startup
    using (var scope = app.Services.CreateScope())
    {
        //Get the registered db context
        var dbContext = scope.ServiceProvider.GetRequiredService<DataBaseContext>();

        // Here is the migration executed
        await dbContext.Database.MigrateAsync();
    }

    // Configure the HTTP request pipeline.
    app.UseSwagger();

    //Use Azure Ad to auth
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId(builder.Configuration[OptionConstants.PwnAzureAdClientId]);
        c.OAuthUsePkce();
        c.OAuthScopeSeparator(" ");
    });

    app.UseAzureAppConfiguration();
    app.UseExceptionHandler("/error");

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.MapHealthChecks("/healthz", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    app.MapHealthChecksUI(options =>
    {
        options.AddCustomStylesheet("dotnet.css");
    });

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    throw;
}
finally
{
    //Write everything into the log
    Log.CloseAndFlush();
}