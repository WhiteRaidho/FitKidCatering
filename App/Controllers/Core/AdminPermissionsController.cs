using FitKidCateringApp.Attributes;
using FitKidCateringApp.Extensions;
using FitKidCateringApp.Helpers;
using FitKidCateringApp.Services.Core;
using FitKidCateringApp.ViewModels.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Controllers.Core
{
    [Authorize]
    [RequireAny(StandardPermissions.AdminAccess)]
    [ApiController]
    [Area("Admin.Core")]
    [Route("api/admin/permissions")]
    public class AdminPermissionsController : Controller
    {
        protected CoreUserService Users { get; }
        protected PermissionsService PermissionsService { get; }

        #region PermissionsController()
        public AdminPermissionsController(CoreUserService usersService, PermissionsService permissionsService)
        {
            Users = usersService;
            PermissionsService = permissionsService;
        }
        #endregion

        #region List()
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GroupViewModel>>> List()
        {
            return Permissions.GetGlobalPermissionsGroups()
                .OrderByDescending(p => p.Value.Count == 1)
                .ThenBy(p => p.Value.Join(" "))
                .Select(p => new GroupViewModel()
                {
                    Key = p.Key.FullName,
                    Name = p.Value.Join(" \\ "),
                    Sections = Enum.GetValues(p.Key)
                        .Cast<object>()
                        .GroupBy(q => (Attribute.GetCustomAttribute(q.GetType().GetField(q.ToString()), typeof(SectionAttribute)) as SectionAttribute)?.Number)
                        .OrderBy(q => q.Key)
                        .Select(q => q
                            .ToDictionary(
                                r => r.ToString(),
                                r => (Attribute.GetCustomAttribute(r.GetType().GetField(r.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description
                            )
                            .ToList()
                        )
                        .ToList()
                })
                .ToList();
        }
        #endregion

        #region Check()
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Dictionary<string, bool>>> Check([FromBody]CheckModel model)
        {
            return model.Permissions.ToDictionary(p => p, p => this.User.HasPermission(Permissions.FromString(p)));
        }
        #endregion

        #region Inspect()
        [HttpGet("inspect/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<KeyValuePair<string, Dictionary<string, string>>>>> Inspect([FromRoute]long id)
        {
            var user = Users.GetCoreUser(id);

            if (user == null)
                return NotFound();

            return Users.GetGlobalPermissions(user);
        }
        #endregion

        #region ChangePermissions()
        [HttpPost("inspect/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ChangePermissions([FromRoute]long id, [FromBody] List<KeyValuePair<string, Dictionary<string, string>>> permissions)
        {
            var user = Users.GetCoreUser(id);

            if (user == null)
                return NotFound();

            Users.ChangePermissions(user, permissions);

            return Ok();
        }
        #endregion
    }
}
