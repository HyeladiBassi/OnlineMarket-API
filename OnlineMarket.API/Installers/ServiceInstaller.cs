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
            services.AddScoped<IAuthHelper, AuthHelper>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<SystemRole>();
            MapperConfiguration mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile(new ProfileMapping()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}