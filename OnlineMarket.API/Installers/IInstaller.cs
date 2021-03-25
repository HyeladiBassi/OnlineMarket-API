using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineMarket.API.Installers
{
    public interface IInstaller
    {
        void InstallServicesAssembly(IServiceCollection services, IConfiguration Configuration);
    }
}