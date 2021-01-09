using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Hahn.ApplicatonProcess.December2020.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Hahn.ApplicatonProcess.December2020.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var configSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configSettings)
                .CreateLogger();

            return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.AddConfiguration(configSettings);
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddSerilog();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

        }
    }

}

