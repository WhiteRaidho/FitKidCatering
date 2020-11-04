using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Offers
{
    public class OfferListItemModel
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Type { get; set; }
        public DateTime DateUtc { get; set; }
        public int DayOfWeek { get; set; }
    }
}
