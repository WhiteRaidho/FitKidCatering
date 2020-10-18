using FitKidCateringApp.Extensions.Security;
using FitKidCateringApp.Extensions.Security.Handlers;
using FitKidCateringApp.Helpers;
using FitKidCateringApp.Models.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions
{
    public static class StartupExtension
    {
        #region RegisterDataServices()
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(p => p.GetName().Name == "FitKidCateringApp")
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
        #endregion

        #region RegisterSecurity()
        public static IServiceCollection RegisterSecurity(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAccessPolicy", policy => policy.RequireAssertion(context =>
                {
                    return context.User.HasPermission(StandardPermissions.AdminAccess);
                }));
            });
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionsHandler>();
            services.AddSingleton<IAuthorizationHandler, OperationsHandler>();

            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(p => p.GetName().Name == "FitKidCateringApp")
                .FirstOrDefault();

            if (assembly != null)
            {
                assembly.GetTypes()
                    .Where(p =>
                        p.Name.EndsWith("Policy") &&
                        p.IsAbstract == false &&
                        p.BaseType != null &&
                        p.BaseType.IsAbstract &&
                        p.BaseType.IsGenericType &&
                        (
                            p.BaseType.GetInterfaces().Contains(typeof(IPermissionsPolicy)) ||
                            p.BaseType.GetInterfaces().Contains(typeof(IOperationsPolicy))
                        )
                    )
                    .ToList()
                    .ForEach(p =>
                        services.AddScoped(p)
                    );
            }

            return services;
        }
        #endregion
    }
}
