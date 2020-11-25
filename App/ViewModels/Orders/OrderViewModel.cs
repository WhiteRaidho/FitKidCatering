using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Orders
{
    public class OrderViewModel
    {
        [Required]
        public Guid ChildPublicId { get; set; }
        [Required]
        public List<Guid> Offers { get; set; }
    }
}
