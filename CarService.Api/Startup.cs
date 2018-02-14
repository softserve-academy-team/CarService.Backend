using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using CarService.Api.Mappers;
using CarService.Api.Services;
using CarService.Api.Models;
using CarService.DbAccess.EF;
using CarService.DbAccess.Entities;
using CarService.DbAccess.DAL;
using Microsoft.EntityFrameworkCore;
using CarService.Api.Security;
using System;

namespace CarService.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = configurationBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => builder.AllowAnyOrigin());
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigin"));
            });

            services.AddSingleton<IConfiguration>(provider => _configuration);
            services.AddSingleton<ICarMapper, AutoRiaCarMapper>();
            services.AddSingleton<ICarService, AutoRiaCarService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IUnitOfWorkFactory>(provider => new SqlUnitOfWorkFactory(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }));

            services.AddDbContext<CarServiceDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CarService.Api")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CarServiceDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                int? httpsPort = null;
                IConfigurationSection httpsSection = _configuration.GetSection("HttpServer:Endpoints:Https");
                if (httpsSection.Exists())
                {
                    var httpsEndpoint = new EndpointConfiguration();
                    httpsSection.Bind(httpsEndpoint);
                    httpsPort = httpsEndpoint.Port;
                }
                app.UseRewriter(new RewriteOptions().AddRedirectToHttps(StatusCodes.Status302Found, httpsPort));
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
