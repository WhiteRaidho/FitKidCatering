using AutoMapper;
using FitKidCateringApp.Models;
using FitKidCateringApp.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Services.Core
{
    public class CoreRolesService : BaseService
    {
        protected PermissionsService Permissions { get; }

        #region RolesService()
        public CoreRolesService(ApplicationDbContext context, IMapper mapper, PermissionsService permissions) : base(context, mapper)
        {
            Permissions = permissions;
        }
        #endregion

        #region GetActive()
        public List<CoreRole> GetActive(IEnumerable<long> ids)
        {
            return Context.CoreRoles
                .Where(p => ids.Contains(p.Id))
                .ToList();
        }
        #endregion
    }
}
