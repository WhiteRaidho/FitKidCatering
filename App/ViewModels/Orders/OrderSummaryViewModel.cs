using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Orders
{
    public class OrderSummaryViewModel
    {
        public Guid OfferId { get; set; }
        public string OfferName { get; set; }
        public int Ammount { get; set; }
    }
}
