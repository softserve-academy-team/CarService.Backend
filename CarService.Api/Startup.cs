using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace CarService.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly AuthOptions _options;

        public Startup(IConfiguration configuration, IHostingEnvironment env, IOptions<AuthOptions> optionsAccessor)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = configurationBuilder.Build();
            _configuration = configuration;
            _options = optionsAccessor.Value;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddOptions();
            services.Configure<AuthOptions>(_configuration.GetSection("AuthOptions"));
            services.Configure<EmailConfig>(_configuration.GetSection("Email"));


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigin"));
            });

            services.AddScoped<IUnitOfWorkFactory>(provider => new SqlUnitOfWorkFactory(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }));

            services.AddDbContext<CarServiceDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CarService.Api")));


            services.AddIdentity<User, IdentityRole>(
                options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                }
            )
                .AddEntityFrameworkStores<CarServiceDbContext>()
                .AddDefaultTokenProviders();

            // JWT
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = _options.Issuer,
                            ValidateAudience = true,
                            ValidAudience = _options.Audience,
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Key)),
                            ValidateIssuerSigningKey = true,
                        };
                    });



            services.AddSingleton<ICarMapper, AutoRiaCarMapper>();
            services.AddSingleton<ICarService, AutoRiaCarService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddSingleton<IConfiguration>(provider => _configuration);
            services.AddTransient<IEmailService, EmailService>();


            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
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
            app.UseCors("AllowAllOrigin");

            app.UseMvc();
        }
    }
}
