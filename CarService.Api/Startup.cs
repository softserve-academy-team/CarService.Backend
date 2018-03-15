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
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Collections.Generic;

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

            services.AddSingleton<IConfiguration>(provider => _configuration);
            services.AddSingleton<ICarMapper, AutoRiaCarMapper>();
            services.AddSingleton<ICarService, AutoRiaCarService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IUnitOfWorkFactory>(provider => new SqlUnitOfWorkFactory(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                // options.UseInMemoryDatabase("CarServiceDb");
            }));

            services.AddDbContext<CarServiceDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CarService.Api")));
                // options.UseInMemoryDatabase("CarServiceDb"));


            services.AddIdentity<User, IdentityRole<int>>(
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

            services.AddMvc();
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Info { Title = "Car Service Web API", Version = "v1", Description = "ASP.NET Core Web API" });
                // swagger.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "SwaggerCarService.xml"));
                swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SwaggerCarService.xml"));

                swagger.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Name = "Authorization",
                    In = "header",
                    Description = "Please insert JWT with Bearer into field. Example: Bearer {token}",
                    Type = "apiKey"
                });
                swagger.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                     {"Bearer", new string[] {} }
                });
            });
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Service Web API");
            });

            app.UseMvc();
        }
    }
}
