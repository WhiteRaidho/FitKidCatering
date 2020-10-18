using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions.Security.Handlers
{
    public class OperationsHandler : AuthorizationHandler<OperationAuthorizationRequirement, object>
    {
        protected IServiceProvider ServiceProvider { get; }
        protected Dictionary<Type, Type> Policies { get; }

        #region OperationsHandler()
        public OperationsHandler(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Policies = this.GetAuthorizationPolicies();
        }
        #endregion

        #region HandleRequirementAsync()
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, object resource)
        {
            var tokens = requirement.Name.Split(":").ToList();
            var type = Type.GetType(tokens.First());
            var value = Enum.Parse(type, tokens.Last());
            var policy = null as IOperationsPolicy;

            if (Policies.ContainsKey(type))
            {
                policy = ServiceProvider.GetService(Policies[type]) as IOperationsPolicy;
            }
            else
            {
                policy = ServiceProvider.GetService<StandardPolicy>() as IOperationsPolicy;
            }

            if (policy.Execute(context, value, resource))
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
}
