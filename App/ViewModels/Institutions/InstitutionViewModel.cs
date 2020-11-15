using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Institutions
{
    public class InstitutionViewModel
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
    }
}
