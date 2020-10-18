using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions.Security
{
    public class StandardPolicy : AuthorizationPolicy<object, object>
    {
        public override bool Handle(ClaimsPrincipal user, object permission)
        {
            return user.HasPermission(permission);
        }

        public override bool Handle(ClaimsPrincipal user, object permission, object resource)
        {
            return user.HasPermission(permission, (resource as IEntity)?.Id ?? 0);
        }
    }
}
