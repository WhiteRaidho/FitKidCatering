using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Institutions
{
    public class InstitutionFormModel
    {
        [Required(ErrorMessage = "Nazwa placówki jest wymagana")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nazwa ulicy jest wymagana")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Nazwa miejscowości jest wymagana")]
        public string City { get; set; }
        [Required(ErrorMessage = "Id użytkownika jest wymagane")]
        public string OwnerId { get; set; }
    }
}
