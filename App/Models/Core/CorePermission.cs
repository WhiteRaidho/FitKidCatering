using FitKidCateringApp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Models.Core
{
    [Table("CorePermissions")]
    public class CorePermission : Entity
    {
        public string AuthorType { get; set; }
        public long AuthorId { get; set; }
        public string PermissionsType { get; set; }
        public string PermissionsJson { get; set; }
        public string ResourceType { get; set; }
        public long? ResourceId { get; set; }
        
        [NotMapped]
        public IReadOnlyDictionary<string, string> Permissions
        {
            get => PermissionsJson.DeserializeJson<Dictionary<string, string>>();
            set => PermissionsJson = value.SerializeJson();
        }
    }
}
