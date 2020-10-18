using FitKidCateringApp.Extensions.Security.Handlers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions.Security
{
    public abstract class AuthorizationPolicy<TEnum> : IPermissionsPolicy
    {
        #region Execute()
        public bool Execute(AuthorizationHandlerContext context, List<object> permissions, RequirePermissions require)
        {
            var result = permissions
                .Cast<TEnum>()
                .Select(permission => Handle(context.User, permission))
                .ToList();

            return require == RequirePermissions.All ?
                result.Where(p => p == false).None() :
                result.Where(p => p == true).Any();
        }
        #endregion

        public abstract bool Handle(ClaimsPrincipal user, TEnum permission);
    }

    public abstract class AuthorizationPolicy<TEnum, TResource> : AuthorizationPolicy<TEnum>, IOperationsPolicy
    {
        #region Execute()
        public bool Execute(AuthorizationHandlerContext context, object permission, object resource)
        {
            return Handle(context.User, (TEnum)permission, (TResource)resource);
        }
        #endregion

        public abstract bool Handle(ClaimsPrincipal user, TEnum permission, TResource resource);
    }

    public interface IPermissionsPolicy
    {
        bool Execute(AuthorizationHandlerContext context, List<object> permissions, RequirePermissions require);
    }

    public interface IOperationsPolicy
    {
        bool Execute(AuthorizationHandlerContext context, object permission, object resource);
    }
}
