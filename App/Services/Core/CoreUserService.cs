using AutoMapper;
using FitKidCateringApp.Models;
using FitKidCateringApp.Models.Core;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Services.Core
{
    public class CoreUserService : BaseService
    {
        IPasswordHasher<CoreUser> Hasher { get; }

        public CoreUserService(
            ApplicationDbContext context,
            IMapper mapper,
            IPasswordHasher<CoreUser> hasher
            ) : base(context, mapper)
        {
            Hasher = hasher;
        }

        public List<CoreUser> GetList()
        {
            var list = Context.CoreUsers
                .ToList();

            return list;
        }

        #region GetCoreUser()
        public CoreUser GetCoreUser(long id)
        {
            var CoreUser = Context.CoreUsers.FirstOrDefault(u => u.Id == id);
            return CoreUser;
        }

        public CoreUser GetCoreUser(string publicId)
        {
            var CoreUser = Context.CoreUsers.FirstOrDefault(u => u.PublicId == Guid.Parse(publicId));
            return CoreUser;
        }

        public async Task<CoreUser> GetCoreUserAsync(long id)
        {
            var CoreUser = await Context.CoreUsers.FindAsync(id);
            return CoreUser;
        }
        #endregion

        #region GetCoreUserByName()
        public CoreUser GetCoreUserByName(string username)
        {
            var CoreUser = Context.CoreUsers.FirstOrDefault(u => u.UserName == username);
            return CoreUser;
        }
        #endregion


        public CoreUser Authenticate(string username, string password)
        {
            var CoreUser = Context.CoreUsers.SingleOrDefault(x => x.UserName == username && x.PasswordHash == password);
            return CoreUser;
        }

        public string GetRefreshToken(CoreUser CoreUser)
        {
            if (String.IsNullOrEmpty(CoreUser.RefreshToken))
            {
                CoreUser.RefreshToken = Hasher.HashPassword(CoreUser, Guid.NewGuid().ToString())
                    .Replace("+", string.Empty)
                    .Replace("=", string.Empty)
                    .Replace("/", string.Empty);

                Update(CoreUser);
            }

            return CoreUser.RefreshToken;
        }

        public CoreUser GetCoreUserByRefreshToken(string token)
        {
            return Context.CoreUsers.SingleOrDefault(x => x.RefreshToken == token);
        }
    }
}
