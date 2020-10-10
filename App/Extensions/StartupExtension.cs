using FitKidCateringApp.Helpers;
using FitKidCateringApp.Models.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(p => p.GetName().Name == "App")
                .FirstOrDefault();

            if (assembly != null)
            {
                assembly.GetTypes()
                    .Where(p => p.Name.EndsWith("Service"))
                    .ToList()
                    .ForEach(p =>
                        services.AddTransient(p)
                    );
            }

            return services;
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<CoreRole>>();
            IdentityResult roleResult;

            foreach (var role in Enum.GetNames(typeof(Role)))
            {
                var roleExist = await RoleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new CoreRole(role));
                }
            }
        }
    }
}
