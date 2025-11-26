using Hcb.Rnd.Pwn.Infrastructure.Services.BackgroundJobs.WashingMachines.Synchronize;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Hcb.Rnd.Pwn.Infrastructure.DI;

/// <summary>
/// https://www.quartz-scheduler.net/documentation/quartz-3.x/quick-start.html#trying-out-the-application
/// https://www.youtube.com/watch?v=p6bZewonoM0
/// </summary>
public static class BackgroundJobsDependencyInjection
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.AddSynchronizeWashingMachinesJob();
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        return services;
    }

    static void AddSynchronizeWashingMachinesJob(this IServiceCollectionQuartzConfigurator serviceCollectionQuartzConfigurator)
    {
        var jobKey = new JobKey(SynchronizeWashingMachinesJob.Name);
        serviceCollectionQuartzConfigurator.AddJob<SynchronizeWashingMachinesJob>(opts => opts.WithIdentity(jobKey));

        // Trigger for 7:00 a.m. (Monday–Friday)
        serviceCollectionQuartzConfigurator.AddTrigger(opts =>
            opts.ForJob(jobKey)
                .WithIdentity($"{SynchronizeWashingMachinesJob.Name}-trigger-7am")
                .WithCronSchedule("0 0 7 ? * MON-FRI")); // 7:00 a.m., weekdays only

        // Trigger for 12:00 p.m. (Monday–Friday)
        serviceCollectionQuartzConfigurator.AddTrigger(opts =>
            opts.ForJob(jobKey)
                .WithIdentity($"{SynchronizeWashingMachinesJob.Name}-trigger-12pm")
                .WithCronSchedule("0 0 12 ? * MON-FRI")); // 12:00 p.m., weekdays only

    }
}
