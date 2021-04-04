using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineMarket.Errors;

namespace OnlineMarket.API.Installers
{
    public class ErrorHandlerInstaller : IInstaller
    {
        public void InstallServicesAssembly(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc()
                .ConfigureApiBehaviorOptions(options => 
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        APIError<ErrorTypes> error = new ErrorBuilder<ErrorTypes>(ErrorTypes.InvalidRequestBody)
                            .SetMessage("Fields contain some errors")
                            .AddField(actionContext.ModelState)
                            .Build();
                        return new BadRequestObjectResult(error);
                    };
                    options.SuppressMapClientErrors = true;
                });
        }
    }
}