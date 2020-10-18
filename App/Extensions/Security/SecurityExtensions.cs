using FitKidCateringApp.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions.Security
{
    public static class SecurityExtensions
    {
        #region Can()
        public static bool Can<TPermission>(this IAuthorizationService service, ClaimsPrincipal user, TPermission permission, object resource)
        {
            var operation = new OperationAuthorizationRequirement()
            {
                Name = $"{permission.GetType().FullName}:{permission.ToString()}"
            };

            return service.AuthorizeAsync(user, resource, operation).Result.Succeeded;
        }

        public static bool Can<TPermission>(this IAuthorizationService service, ClaimsPrincipal user, TPermission permission)
        {
            return service.AuthorizeAsync(user, new RequireAllAttribute(permission).Policy).Result.Succeeded;
        }
        #endregion

        #region GetAuthorizationPolicies()
        public static Dictionary<Type, Type> GetAuthorizationPolicies(this IAuthorizationHandler handler)
        {
            var handlers = new Dictionary<Type, Type>();
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
                        handlers.TryAdd(p.BaseType.GetGenericArguments().First(), p)
                    );
            }

            return handlers;
        }
        #endregion
    }
}
