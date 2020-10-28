using FitKidCateringApp.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FitKidCateringApp.Models.Core
{
    [Table("CoreUsers")]
    public class CoreUser : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PublicId { get; set; } = Guid.NewGuid();

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public string RolesJson { get; set; }

        [NotMapped]
        public IReadOnlyList<long> Roles
        {
            get => RolesJson.DeserializeJson<List<long>>();
            set => RolesJson = value.Select(p => p.ToString()).SerializeJson();
        }

        [NotMapped]
        public List<KeyValuePair<string, Dictionary<string, string>>> Permissions { get; set; } = new List<KeyValuePair<string, Dictionary<string, string>>>();
    }
}
