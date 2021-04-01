using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineMarket.DataAccess;
using OnlineMarket.Models;
using System;

namespace OnlineMarket.API.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServicesAssembly(IServiceCollection services, IConfiguration configuration)
        {
            var connectString = configuration["ConnectionStrings:onlineConnect"];
            services.AddDbContext<DataContext>(
                item => item.UseSqlite(connectString, b => b.MigrationsAssembly("OnlineMarket.API")));
            services.AddDefaultIdentity<SystemUser>(options =>
                {
                    // Password Settings
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 5;

                    // Lockout Settings
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                    // User Settings
                    options.User.RequireUniqueEmail = true;
                }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

        }
    }
}