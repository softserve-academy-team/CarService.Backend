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

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using CarService.DbAccess.EF;

namespace CarService.Api
{

    public class UserDbContext : IdentityDbContext<IdentityUser>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options): base(options)
        {
            
        }
    }


    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly AuthOptions _options;
        
        public Startup(IConfiguration configuration,IHostingEnvironment env, IOptions<AuthOptions> optionsAccessor)
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

            services.Configure<AuthOptions>(_configuration.GetSection("AuthOptions"));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    
                    });
            });
          
            services.AddDbContext<CarServiceDbContext>(opt => opt.UseInMemoryDatabase("user"));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();
            services.AddMvc();

			// JWT
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = _options.Issuer,
 
                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = _options.Audience,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,
 
                            // установка ключа безопасности
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Key)),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });


            services.AddSingleton<ICarMapper, AutoRiaCarMapper>();
            services.AddSingleton<ICarService, AutoRiaCarService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IConfiguration>(provider => _configuration);

            services.AddDbContext<CarServiceDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
 
            services.AddMvc();
           
          

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
