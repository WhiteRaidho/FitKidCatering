using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions.Security.Handlers
{
    public class PermissionsHandler : AuthorizationHandler<PermissionsRequirement>
    {
        protected IServiceProvider ServiceProvider { get; }
        protected Dictionary<Type, Type> Policies { get; }

        #region PermissionsHandler()
        public PermissionsHandler(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Policies = this.GetAuthorizationPolicies();
        }
        #endregion

        #region HandleRequirementAsync()
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsRequirement requirement)
        {
            var type = requirement.Permissions.First().GetType();
            var policy = null as IPermissionsPolicy;

            if (Policies.ContainsKey(type))
            {
                policy = ServiceProvider.GetService(Policies[type]) as IPermissionsPolicy;
            }
            else
            {
                policy = ServiceProvider.GetService<StandardPolicy>() as IPermissionsPolicy;
            }

            if (policy.Execute(context, requirement.Permissions, requirement.Require))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
        #endregion
    }

    public class PermissionsRequirement : IAuthorizationRequirement
    {
        public RequirePermissions Require { get; set; } = RequirePermissions.Any;
        public List<object> Permissions { get; set; } = new List<object>();

        #region PermissionsRequirement()
        public PermissionsRequirement(RequirePermissions require, List<object> permissions)
        {
            Require = require;
            Permissions = permissions;
        }
        #endregion
    }

    public enum RequirePermissions
    {
        Any,
        All
    }
}
