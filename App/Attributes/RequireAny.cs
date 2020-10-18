using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Attributes
{
    public class RequireAnyAttribute : AuthorizeAttribute
    {
        #region RequireAnyAttribute()
        public RequireAnyAttribute(params object[] permissions)
        {
            if (permissions.Length == 0)
                throw new ArgumentException("Yout have to pass at least one permission.");

            var types = permissions.Select(p => p.GetType()).Distinct();

            if (types.Count() > 1)
                throw new ArgumentException("All permission must be of the same enum type.");

            this.Policy = String.Format("RequireAny:{0}:{1}", types.First().FullName, permissions
                .Select(p =>
                    Enum.GetName(p.GetType(), p)
                )
                .Join(",")
            );
        }
        #endregion
    }
}
