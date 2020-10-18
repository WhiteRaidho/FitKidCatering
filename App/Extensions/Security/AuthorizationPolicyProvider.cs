using FitKidCateringApp.Extensions.Security.Handlers;
using FitKidCateringApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions.Security
{
    public class AuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        const string REQUIRE_ALL_PREFIX = "RequireAll:";
        const string REQUIRE_ANY_PREFIX = "RequireAny:";

        protected DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        #region AuthorizationPolicyProvider()
        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }
        #endregion

        #region GetDefaultPolicyAsync()
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return FallbackPolicyProvider.GetDefaultPolicyAsync();
        }
        #endregion

        #region GetPolicyAsync()
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(REQUIRE_ALL_PREFIX))
            {
                var permissions = GetPermissions(policyName, REQUIRE_ALL_PREFIX);
                var policy = GetBuilder(RequirePermissions.All, permissions).Build();

                return Task.FromResult(policy);
            }
            if (policyName.StartsWith(REQUIRE_ANY_PREFIX))
            {
                var permissions = GetPermissions(policyName, REQUIRE_ANY_PREFIX);
                var policy = GetBuilder(RequirePermissions.Any, permissions).Build();

                return Task.FromResult(policy);
            }

            return GetDefaultPolicyAsync();
        }
        #endregion

        #region GetPermissions()
        private List<object> GetPermissions(string policyName, string prefix)
        {
            var tokens = policyName.Substring(prefix.Length).Split(":");
            var enumType = Permissions.GetGlobalPermissionsTypes()
                .Where(p => p.FullName == tokens.First())
                .First();

            return tokens
                .Last()
                .Split(',')
                .Where(p => p.Trim().Length > 0)
                .Select(p => Enum.Parse(enumType, tokens.Last()))
                .ToList();
        }
        #endregion

        #region GetBuilder()
        private AuthorizationPolicyBuilder GetBuilder(RequirePermissions require, List<object> permissions)
        {
            return new AuthorizationPolicyBuilder().AddRequirements(new PermissionsRequirement(require, permissions));
        }
        #endregion
    }
}
