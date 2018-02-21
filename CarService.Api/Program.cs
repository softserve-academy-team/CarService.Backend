using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using CarService.Api.Security;
using System.Net;

namespace CarService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
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
