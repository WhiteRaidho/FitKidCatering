using FitKidCateringApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions
{
    public static class IdentityExtension
    {
        #region Id()
        public static long Id(this IPrincipal principal)
        {
            return Convert.ToInt64((principal as ClaimsPrincipal).FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static long Id(this IIdentity identity)
        {
            return Convert.ToInt64((identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        #endregion

        #region AddPermission()
        public static void AddPermission(this ClaimsPrincipal principal, object permission, long resource = 0)
        {
            principal.AddPermission(permission.GetType().FullName, permission.ToString(), resource);
        }

        public static void AddPermission(this ClaimsPrincipal principal, string typeName, string permissionName, long resource = 0)
        {
            (principal.Identity as ClaimsIdentity).AddClaim(new Claim(typeName, $"{permissionName}:{resource}"));
        }
        #endregion

        #region HasPermission()
        public static bool HasPermission(this ClaimsPrincipal principal, object permission, long resource = 0)
        {
            return principal.HasPermission(permission.GetType(), permission.ToString(), resource);
        }

        public static bool HasPermission(this ClaimsPrincipal principal, Type permissionType, string permissionName, long resource = 0)
        {
            var identity = principal.Identity as ClaimsIdentity;

            return
                identity.HasClaim(p => p.Type == typeof(StandardPermissions).FullName && p.Value == $"{StandardPermissions.AdminAccess.ToString()}:0") ||
                identity.HasClaim(p => p.Type == permissionType.FullName && p.Value == $"{permissionName}:0") ||
                identity.HasClaim(p => p.Type == permissionType.FullName && p.Value == $"{permissionName}:{resource}");
        }
        #endregion
    }
}
