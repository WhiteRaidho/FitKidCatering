using AutoMapper;
using FitKidCateringApp.Extensions;
using FitKidCateringApp.Models;
using FitKidCateringApp.Models.Core;
using FitKidCateringApp.ViewModels.Core;
using LinqKit;
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
        protected PermissionsService Permissions { get; }
        protected CoreRolesService CoreRoles { get; }

        public CoreUserService(
            ApplicationDbContext context,
            IMapper mapper,
            IPasswordHasher<CoreUser> hasher,
            PermissionsService permissions,
            CoreRolesService coreRoles
            ) : base(context, mapper)
        {
            Hasher = hasher;
            Permissions = permissions;
            CoreRoles = coreRoles;
        }

        public List<CoreUser> GetList(int limit = 0, UserFilter filter = null)
        {
            var predicate = PredicateBuilder.New<CoreUser>(true);

            if(!String.IsNullOrEmpty(filter.Email))
            {
                predicate.And(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
            }

            if (!String.IsNullOrEmpty(filter.Name))
            {
                predicate.And(x => x.FirstName.ToLower().Contains(filter.Name.ToLower()) || x.LastName.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!String.IsNullOrEmpty(filter.UserName))
            {
                predicate.And(x => x.UserName.ToLower().Contains(filter.UserName.ToLower()));
            }

            var list = Context.CoreUsers
                .Where(predicate);

            if(limit > 0)
            {
                list = list.Take(limit);
            }

            return list.ToList();
        }

        #region GetCoreUser()
        public CoreUser GetCoreUser(long id)
        {
            var CoreUser = Context.CoreUsers.FirstOrDefault(u => u.Id == id);
            return CoreUser;
        }

        public CoreUser GetCoreUser(Guid publicId)
        {
            var CoreUser = Context.CoreUsers.FirstOrDefault(u => u.PublicId == publicId);
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

        #region GetGlobalPermissions()
        public List<KeyValuePair<string, Dictionary<string, string>>> GetGlobalPermissions(CoreUser user)
        {
            return Permissions.GetGlobalPermissions(GetAuthors(user));
        }
        #endregion

        #region ChangePermissions()
        public void ChangePermissions(CoreUser entity, List<KeyValuePair<string, Dictionary<string, string>>> permissions)
        {
            entity.Permissions = Permissions.ChangeGlobalPermissions(entity, permissions);
            SaveChanges();
        }
        #endregion

        #region GetAuthors()
        private List<Entity> GetAuthors(CoreUser user)
        {
            var authors = new List<Entity>();

            authors.AddRange(CoreRoles.GetActive(user.Roles).Cast<Entity>().ToList());
            authors.Add(user);

            return authors;
        }
        #endregion
    }
}
