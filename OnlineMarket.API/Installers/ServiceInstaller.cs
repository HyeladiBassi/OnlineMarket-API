using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineMarket.API.MappingProfiles;
using OnlineMarket.Models;
using OnlineMarket.Services.AuthHelper;
using OnlineMarket.Services.Interfaces;
using OnlineMarket.Services.Main;

namespace OnlineMarket.API.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServicesAssembly(IServiceCollection services, IConfiguration configuration)
        {
            #region Cors Policy

            services.AddCors(options => options.AddPolicy("MarketPolicy",
            builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            }));

            #endregion

            services.AddScoped<IAuthHelper, AuthHelper>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductReviewService, ProductReviewService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddTransient<SystemRole>();
            // MapperConfiguration mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new ProfileMapping()));
            // IMapper mapper = mappingConfig.CreateMapper();
            // services.AddSingleton(mapper);
            services.AddControllers();

        }
    }
}