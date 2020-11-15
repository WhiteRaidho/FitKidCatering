using FitKidCateringApp.Attributes;
using FitKidCateringApp.Models.Offers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Offers
{
    public class OfferFormModel
    {
        [Required(ErrorMessage = "Nazwa posiłku jest wymagana")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Cena posiłku jest wymagana")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena nie może być mniejsza niż {1}")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Typ posiłku jest wymagany")]
        public OfferType Type { get; set; }

        [Required(ErrorMessage = "Dzień posiłku jest wymagana")]
        [Range(0, 6, ErrorMessage = "Dzień posiłku zawierać się w przedziale od poniedziałku do niedzieli")]
        public short DayOfWeek { get; set; }
    }
}
