using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineMarket.API.Installers
{
    public class SendGridInstaller : IInstaller
    {
        public void InstallServicesAssembly(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}