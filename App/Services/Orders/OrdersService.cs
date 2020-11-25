using AutoMapper;
using FitKidCateringApp.Models;
using FitKidCateringApp.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Services.Orders
{
    public class OrdersService : BaseService
    {
        #region OrdersService()
        public OrdersService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        #endregion

        public Order GetByChildId(long childId)
        {
            return Context.Orders
                .FirstOrDefault(x => x.ChildId == childId);
        }
    }
}
