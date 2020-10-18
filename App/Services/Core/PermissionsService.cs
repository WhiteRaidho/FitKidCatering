using FitKidCateringApp.Extensions;
using FitKidCateringApp.Helpers;
using FitKidCateringApp.Models;
using FitKidCateringApp.Models.Core;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Services.Core
{
    public class PermissionsService
    {
        protected ApplicationDbContext Context { get; private set; }

        #region PermissionsService()
        public PermissionsService(ApplicationDbContext context)
        {
            Context = context;
        }
        #endregion

        public List<CorePermission> GetPermissions(Entity author, PermissionType permissionType = PermissionType.All, long? resourceId = null)
        {
            return GetPermissions(new List<Entity>() { author }, permissionType, resourceId);
        }

        public List<CorePermission> GetPermissions(IEnumerable<Entity> authors, PermissionType permissionType = PermissionType.All, long? resourceId = null)
        {
            var predicate = PredicateBuilder.New<CorePermission>(false);

            authors.ForEach((author, index) =>
            {
                predicate = predicate.Or(p => p.AuthorType == author.GetType().FullName && p.AuthorId == author.Id);
            });

            var query = Context.CorePermissions
                .AsExpandable()
                .Where(predicate);

            if (permissionType == PermissionType.Global)
            {
                query = query.Where(p => p.ResourceId.HasValue == false);
            }
            if (permissionType == PermissionType.Resource)
            {
                query = query.Where(p => p.ResourceId.HasValue == true);

                if (resourceId.HasValue)
                {
                    query = query.Where(p => p.ResourceId == resourceId);
                }
            }

            return query.ToList();
        }

        public List<KeyValuePair<string, Dictionary<string, string>>> GetGlobalPermissions(IEnumerable<Entity> authors)
        {
            return GetEffectivePermissions(authors, PermissionType.Global)
                .Select(p => new KeyValuePair<string, Dictionary<string, string>>(p.Key, p.Value[0]))
                .ToList();
        }

        public List<KeyValuePair<string, Dictionary<long, Dictionary<string, string>>>> GetEffectivePermissions(IEnumerable<Entity> authors, PermissionType permissionType = PermissionType.All, long? resourceId = null)
        {
            var result = GetPermissions(authors, permissionType, resourceId)
                .GroupBy(p => p.PermissionsType)
                .ToList();

            return result
                .Select(permissions =>
                {
                    var values = permissions
                        .GroupBy(x => x.ResourceId)
                        .Select(resource =>
                        {
                            var item = new KeyValuePair<long, Dictionary<string, string>>(resource.Key ?? 0, new Dictionary<string, string>());

                            resource
                                .SelectMany(q => q.Permissions.ToList())
                                .Where(q => q.Value == PermissionState.Allow.ToString())
                                .ToList()
                                .ForEach(q => item.Value[q.Key] = q.Value);

                            resource
                                .SelectMany(q => q.Permissions.ToList())
                                .Where(q => q.Value == PermissionState.Deny.ToString())
                                .ToList()
                                .ForEach(q => item.Value[q.Key] = q.Value);

                            return item;
                        })
                        .ToDictionary(p => p.Key, p => p.Value);

                    return new KeyValuePair<string, Dictionary<long, Dictionary<string, string>>>(permissions.Key, values);
                })
                .ToList();
        }
    }

    public enum PermissionType
    {
        All,
        Global,
        Resource
    }
}
