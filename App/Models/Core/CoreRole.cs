using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Models.Core
{
    public class CoreRole : IdentityRole<long>
    {
        public CoreRole() : base()
        {
        }

        public CoreRole(string roleName) : base(roleName)
        {
        }
    }
}
