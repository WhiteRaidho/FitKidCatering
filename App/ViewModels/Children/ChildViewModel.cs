using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Children
{
    public class ChildViewModel
    {
        [Required]
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        public long ParentPublicId { get; set; }
        public long InstitutionPublicId { get; set; }
    }
}
