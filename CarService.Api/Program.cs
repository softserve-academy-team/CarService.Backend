using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using CarService.Api.Security;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using CarService.DbAccess.EF;
using CarService.Api.Helpers;
using Microsoft.Extensions.Logging;

namespace CarService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<CarServiceDbContext>();
                    InitOrderTable.InitializeOrder(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == EnvironmentName.Development)
            {
              webHostBuilder.UseKestrel(options => options.ConfigureEndpoints());
            }

            return webHostBuilder.Build();
        }
    }
}
