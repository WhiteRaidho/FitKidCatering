using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Orders
{
    public class OrderFormModel
    {
        public List<Guid> Offers { get; set; }

        public string Comment { get; set; }
    }
}
