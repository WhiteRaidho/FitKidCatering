using FitKidCateringApp.Helpers;
using FitKidCateringApp.Models.Children;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions.Security
{
    public static class ChildrenAuthorizationHandler
    {
        public static bool HandleChildrenAuthorizationPolicy(this IPermissionsPolicy policy, ClaimsPrincipal user, Type permissionType, string permissionName)
        {
            switch(permissionName)
            {
                default:
                    return user.HasPermission(permissionType, permissionName);
            }
        }

        public static bool HandleProjectsAuthorizationPolicy<TResource>(this IOperationsPolicy policy, ClaimsPrincipal user, Type permissionType, string permissionName, TResource resource) where TResource : Child
        {
            switch(permissionName)
            {
                case "View":
                    return user.HasPermission(typeof(StandardPermissions), "CateringEmployee") || resource.ParentId == user.Id() || resource.Institution.OwnerId == user.Id();
                case "Edit":
                    return user.HasPermission(typeof(StandardPermissions), "CateringEmployee") || resource.ParentId == user.Id() || resource.Institution.OwnerId == user.Id();
                case "Manage":
                    return user.HasPermission(typeof(StandardPermissions), "CateringEmployee") || resource.Institution.OwnerId == user.Id();
                default:
                    return false;
            }
        }
}
