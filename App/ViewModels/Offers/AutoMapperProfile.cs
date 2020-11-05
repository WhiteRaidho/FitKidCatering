using AutoMapper;
using FitKidCateringApp.Models.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Offers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : base()
        {
           OffersProfile();

        }

        #region OffersProfile()
        protected void OffersProfile()
        {
            CreateMap<Offer, OfferListItemModel>()
                .ForMember(d => d.DayOfWeek, o => o.MapFrom(s => s.DateUtc.DayOfWeek));

            CreateMap<OfferFormModel, Offer>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.PublicId, o => o.Ignore());
        }
        #endregion
    }
}
