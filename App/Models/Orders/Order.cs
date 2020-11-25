using FitKidCateringApp.Extensions;
using FitKidCateringApp.Models.Children;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Models.Orders
{
    [Table("Orders")]
    public class Order : Entity
    {
        [Required]
        public long ChildId { get; set; }
        public string OffersJson { get; set; }


        public virtual Child Child { get; set; }
        [NotMapped]
        public IReadOnlyList<long> Offers
        {
            get => OffersJson.DeserializeJson<List<long>>();
            set => OffersJson = value.Select(p => p.ToString()).SerializeJson();
        }
    }
}
