using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using WorkerScheduler;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHangfire(config => config.UseInMemoryStorage());
        services.AddHangfireServer();
        services.AddHostedService<WorkerA>();
        services.AddHostedService<WorkerB>();
    })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build();

await host.RunAsync();

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", context =>
            {
                context.Response.Redirect("/hangfire");
                return Task.CompletedTask;
            });
            endpoints.MapHangfireDashboard();
        });
    }
}
