using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace OnlineMarket.API.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServicesAssembly(IServiceCollection services, IConfiguration configuration)
        {

            services.ConfigureSwaggerGen(setupAction => {
                setupAction.DescribeAllParametersInCamelCase();
            });
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OnlineMarket.API",
                    Version = "v1",
                    Description = "API Made for Online Market.",
                    Contact = new OpenApiContact()
                    {
                        Email = "bassihyeladi@gmail.com",
                        Name = "Newhihi Ltd.",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                Dictionary<string, IEnumerable<string>> security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[0]}
                };
                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Autherization Header using the bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }},new List<string>()}
                });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setupAction.IncludeXmlComments(xmlPath);
            });
        }
    }
}