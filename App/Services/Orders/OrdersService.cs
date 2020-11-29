using AutoMapper;
using FitKidCateringApp.Extensions;
using FitKidCateringApp.Models;
using FitKidCateringApp.Models.Orders;
using FitKidCateringApp.Services.Offers;
using FitKidCateringApp.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Services.Orders
{
    public class OrdersService : BaseService
    {
        protected OffersService Offers { get; }
        #region OrdersService()
        public OrdersService(ApplicationDbContext context, IMapper mapper, OffersService offers) : base(context, mapper)
        {
            Offers = offers;
        }
        #endregion

        public Order GetByChildId(long childId)
        {
            return Context.Orders
                .FirstOrDefault(x => x.ChildId == childId);
        }

        public List<OrderSummaryViewModel> GetSummary()
        {
            var offers = new List<long>();
            Context.Orders.ToList().Select(x => x.Offers)
                .ForEach((x, index) => offers.AddRange(x.ToList()));
            return offers.GroupBy(x => x)
                .Select(group => new OrderSummaryViewModel()
                {
                    Ammount = group.Count(),
                    OfferId = Offers.GetById(group.Key).PublicId,
                    OfferName = Offers.GetById(group.Key).Name
                }).ToList();
        }
        public List<OrderSummaryViewModel> GetSummaryForInstitution(Guid institutuinPublicId)
        {
            var institution = Context.Institutions.FirstOrDefault(x => x.PublicId == institutuinPublicId);
            if (institution == null) return new List<OrderSummaryViewModel>();

            var childIds = Context.Children.Where(x => x.InstitutionId == institution.Id).Select(x => x.Id).ToList();

            var offers = new List<long>();

            Context.Orders
                .Where(x => childIds.Contains(x.ChildId))
                .ToList().Select(x => x.Offers)
                .ForEach((x, index) => offers.AddRange(x.ToList()));

            return offers.GroupBy(x => x)
                .Select(group => new OrderSummaryViewModel()
                {
                    Ammount = group.Count(),
                    OfferId = Offers.GetById(group.Key).PublicId,
                    OfferName = Offers.GetById(group.Key).Name
                }).ToList();
        }
    }
}
