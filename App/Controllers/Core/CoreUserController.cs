using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitKidCateringApp.Services.Core;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using FitKidCateringApp.Models.Core;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using FitKidCateringApp.ViewModels.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace FitKidCateringApp.Controllers.Core
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class CoreUserController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected IPasswordHasher<CoreUser> Hasher { get; }
        protected CoreUserService Users { get; }
        protected IConfiguration Configuration { get; }

        public CoreUserController(IMapper mapper, IPasswordHasher<CoreUser> hasher, CoreUserService usersService, IConfiguration configuration)
        {
            Mapper = mapper;
            Users = usersService;
            Configuration = configuration;
            Hasher = hasher;
        }

        #region Authenticate()
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = Users.GetCoreUserByName(model.Username);

            if(user != null)
            {
                var result = Hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
                var token = CreateToken(user);
                return Ok(token);
            }

            return BadRequest(new { message = "Login lub hasło są nieprawidłowe" });
        }

        private TokenViewModel CreateToken(CoreUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    //new Claim(JwtRegisteredClaimNames.Jti, "1"),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = creds
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return new TokenViewModel()
            {
                Token = user.Token,
                Refresh = Users.GetRefreshToken(user),
                Expires = token.ValidTo
            };
        }
        #endregion

        #region RefreshToken()
        [HttpGet("authenticate/refresh")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TokenViewModel>> RefreshToken([FromHeader(Name = "Authorization")]string authorization)
        {
            if (!String.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            {
                var jwtVal = authorization.Replace("Bearer ", String.Empty);
                var jwt = new JwtSecurityTokenHandler().ReadToken(jwtVal) as JwtSecurityToken;

                if (jwt != null)
                {
                    long userId = Int64.Parse(jwt.Claims.ToList()[0].Value);
                    CoreUser user = Users.GetCoreUser(userId);

                    if (user != null) return CreateToken(user);
                }
            }
            return BadRequest(new { message = "Bad authorization method" });
        }

        [AllowAnonymous]
        [HttpPost("authenticate/recover")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TokenViewModel>> RecoverToken([FromBody]TokenRequestViewModel model)
        {
            if (!String.IsNullOrEmpty(model.Token))
            {
                var user = Users.GetCoreUserByRefreshToken(model.Token);

                if (user != null) return CreateToken(user);
            }

            return BadRequest(new { message = "Błędny token odświeżania" });
        }
        #endregion

        #region Register()
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            var user = Users.GetCoreUserByName(model.UserName);
            if (user != null) return BadRequest(new { message = "Taki użytkownik już istnieje" });

            CoreUser u = Mapper.Map<CoreUser>(model);
            u.PasswordHash = Hasher.HashPassword(u, model.Password);

            user = Users.Create(u);

            if (user == null) return BadRequest(new { message = "Coś poszło nie tak" });

            return Ok();
        }
        #endregion
    }
}
