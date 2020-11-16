using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Institutions
{
    public class InstitutionListItemModel
    { 
        public string PublicId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string OwnerPublicId { get; set; }
        public string OwnerUsername { get; set; }
    }
}
