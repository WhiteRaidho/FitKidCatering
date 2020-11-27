using AutoMapper;
using FitKidCateringApp.Attributes;
using FitKidCateringApp.Helpers;
using FitKidCateringApp.Services.Core;
using FitKidCateringApp.ViewModels.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Controllers.Core
{
    [Authorize]
    [ApiController]
    [Area("Admin.Core")]
    [Route("api/admin/users")]
    public class AdminCoreUserController : Controller
    {
        protected IAuthorizationService Authorization { get; }
        protected IMapper Mapper { get; }
        protected CoreUserService Users { get; }

        public AdminCoreUserController(IAuthorizationService authorization, IMapper mapper, CoreUserService users)
        {
            Authorization = authorization;
            Mapper = mapper;
            Users = users;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UserListItemModel>>> GetList([FromQuery] UserFilter filter, [FromQuery] int limit = 0)
        {
            var items = Users.GetList(limit, filter);
            var result = Mapper.Map<IEnumerable<UserListItemModel>>(items);

            return Ok(result);
        }
    }
}
