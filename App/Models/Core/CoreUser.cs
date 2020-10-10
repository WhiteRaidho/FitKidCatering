using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitKidCateringApp.Models.Core
{
    [Table("CoreUsers")]
    public class CoreUser : IdentityUser<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PublicId { get; set; } = Guid.NewGuid();
        
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
