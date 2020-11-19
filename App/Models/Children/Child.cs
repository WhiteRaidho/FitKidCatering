using FitKidCateringApp.Extensions;
using FitKidCateringApp.Models.Core;
using FitKidCateringApp.Models.Institutions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Models.Children
{
    [Table("Children")]
    public class Child : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PublicId { get; set; } = Guid.NewGuid();

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public long ParentId { get; set; }

        [Required]
        public long InstitutionId { get; set; }

		public virtual CoreUser Parent {get; set; }

        public virtual Institution Institution { get; set; }
    }
}
