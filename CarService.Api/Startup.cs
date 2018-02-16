using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using CarService.Api.Mappers;
using CarService.Api.Services;
using CarService.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CarService.DbAccess.EF;
using Microsoft.Extensions.Options;
using System.Text;

namespace CarService.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly AuthOptions _options;
        
        public Startup(IConfiguration configuration, IOptions<AuthOptions> optionsAccessor)
        {
            _configuration = configuration;
            _options = optionsAccessor.Value;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<AuthOptions>(_configuration.GetSection("AuthOptions"));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => builder.AllowAnyOrigin());
            });
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigin"));
            });

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

            services.AddDbContext<CarServiceDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
 
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ICarService carService)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
