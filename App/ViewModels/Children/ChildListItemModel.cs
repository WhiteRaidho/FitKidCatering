using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Children
{
    public class ChildListItemModel
    {
        public string PublicId { get; set; }
        public string Name { get; set; }
        public string ParentPublicId { get; set; }
        public string ParentUsername { get; set; }
        public string InstitutionPublicId { get; set; }
        public string InstitutionName { get; set; }
    }
}
