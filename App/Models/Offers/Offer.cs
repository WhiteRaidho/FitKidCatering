using FitKidCateringApp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Models.Offers
{
    [Table("Offers")]
    public class Offer : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PublicId { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public OfferType Type { get; set; }
        public short DayOfWeek { get; set; }
    }

    public enum OfferType
    {
        Breakfast = 1,
        OnePartDinner = 2,
        TwoPartDinner = 3,
        Snack = 4
    }
}
