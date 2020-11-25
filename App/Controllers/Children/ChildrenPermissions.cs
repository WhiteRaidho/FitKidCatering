using FitKidCateringApp.Extensions.Security;
using FitKidCateringApp.Helpers;
using FitKidCateringApp.Models.Children;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitKidCateringApp.Controllers.Children
{
    [GlobalPermissions("Dzieci")]
    public enum ChildrenPermissions
    {
        [Description("Widzi dane o dziecku")]
        View,

        [Description("Może edytować informacje o dziecku")]
        Edit,

        [Description("Może zarządzać listą dzieci")]
        Manage,

    }

    public class ChildrenPolicy : AuthorizationPolicy<ChildrenPermissions, Child>
    {
        public override bool Handle(ClaimsPrincipal user, ChildrenPermissions permission)
        {
            return this.HandleChildrenAuthorizationPolicy(user, permission.GetType(), permission.ToString());
        }

        public override bool Handle(ClaimsPrincipal user, ChildrenPermissions permission, Child resource)
        {
            return this.HandleProjectsAuthorizationPolicy(user, permission.GetType(), permission.ToString(), resource);
        }
    }
}
