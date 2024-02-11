using Hangfire;
using Microsoft.Extensions.Hosting;

namespace WorkerScheduler
{
    public class WorkerB : IHostedService
    {
        private readonly IRecurringJobManager _recurringJobManager;

        public WorkerB(IRecurringJobManager recurringJobManager)
        {
            _recurringJobManager = recurringJobManager;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Schedule a recurring job
            _recurringJobManager.AddOrUpdate(
                "WorkerB", // A unique identifier for the recurring job
                () => Console.WriteLine("Hello, world from Hangfire recurring job!"), // The method to call
                Cron.Minutely); // CRON expression for scheduling, e.g., once a day

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}