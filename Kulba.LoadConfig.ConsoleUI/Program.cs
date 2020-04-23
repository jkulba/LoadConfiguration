using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Kulba.LoadConfig.ConsoleUI
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Services.GetService<IApplication>().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
               .ConfigureAppConfiguration((hostContext, configAppBuilder) =>
               {
                   configAppBuilder.AddJsonFile("appsettings.json", optional: false);
                   configAppBuilder.AddEnvironmentVariables();
                   configAppBuilder.AddCommandLine(args);
               })
               .ConfigureServices((hostContext, services) =>
               {
                   services.Configure<AppConfigInfo>(hostContext.Configuration.GetSection("AppConfigInfo"));
                   services.AddTransient<IApplication, Application>();
               })
               .UseSerilog((hostContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostContext.Configuration)
               )
               .UseConsoleLifetime(); 
    }
}
