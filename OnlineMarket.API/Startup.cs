using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OnlineMarket.API.Installers;
using OnlineMarket.DataAccess;
using OnlineMarket.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace OnlineMarket.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.InstallServicesAssembly(Configuration);
            services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<SystemUser> userManager, RoleManager<IdentityRole> roleManager, DataContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineMarket.API Version 1");
                setupAction.DocExpansion(DocExpansion.None);
            });

            app.UseCors("MarketPolicy");
            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();


            app.UseAuthorization();

            app.UseAuthentication();

            DataInitializer.SeedDatabase(userManager, roleManager, context).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
