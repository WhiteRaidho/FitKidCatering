using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Children
{
    public class ChildFormModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid ParentPublicId { get; set; }
        public Guid InstitutionPublicId { get; set; }
    }
}
