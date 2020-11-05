using FitKidCateringApp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Models.Core
{
    [Table("Institutions")]
    public class Institution : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PublicId { get; set; } = Guid.NewGuid();

        [Required, MaxLength(32, ErrorMessage = "Name is too long (max 32 characters)")]
        public string Name { get; set; }

        [Required, RegularExpression(@"\d{2}-\d{3}")]
        public string ZipCode { get; set; }

        [Required, MaxLength(64, ErrorMessage = "Street name is too long (max 64 characters)")]
        public string Street { get; set; }

        [Required, MaxLength(32, ErrorMessage = "City name is too long (max 32 characters)")]
        public string City { get; set; }

        [Required]
        public long OwnerId { get; set; }

        public virtual CoreUser Owner { get; set; }
    }
}
