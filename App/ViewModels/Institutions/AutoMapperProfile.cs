using AutoMapper;
using FitKidCateringApp.Models.Institutions;
using FitKidCateringApp.ViewModels.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitKidCateringApp.Models.Core;

namespace FitKidCateringApp.ViewModels.Institutions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : base()
        {
            InstitutionProfile();
        }

        #region InstitutionProfile()
        protected void InstitutionProfile()
        {
            CreateMap<Institution, InstitutionViewModel>();
            CreateMap<InstitutionFormModel, Institution>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.PublicId, o => o.Ignore());
            CreateMap<Institution, InstitutionListItemModel>()
                .ForMember(d => d.OwnerPublicId, o => o.MapFrom(s => s.Owner.PublicId))
                .ForMember(d => d.OwnerUsername, o => o.MapFrom(s => s.Owner.UserName));
        }
        #endregion
    }
}