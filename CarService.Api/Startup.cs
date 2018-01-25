using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CarService.Api.Services;

namespace CarService.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            services.AddSingleton<ICarUrlBuilder, AutoRiaCarUrlBuilder>();
            services.AddSingleton<ICarMapper, AutoRiaCarMapper>();            
            services.AddSingleton<ICarService, AutoRiaCarService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ICarService carService)
        {
            app.UseCors(builder => builder.AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
