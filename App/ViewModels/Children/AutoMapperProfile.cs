using AutoMapper;
using FitKidCateringApp.Models.Children;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.ViewModels.Children
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : base()
        {
            ChildProfile();
        }

        #region ChildProfile()
        protected void ChildProfile()
        {
            CreateMap<Child, ChildListItemModel>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.FirstName + ' ' + s.LastName))
                .ForMember(d => d.ParentPublicId, o => o.MapFrom(s => s.Parent.PublicId))
                .ForMember(d => d.ParentUsername, o => o.MapFrom(s => s.Parent.UserName))
                .ForMember(d => d.InstitutionPublicId, o => o.MapFrom(s => s.Institution.PublicId))
                .ForMember(d => d.InstitutionName, o => o.MapFrom(s => s.Institution.Name));

            CreateMap<ChildFormModel, Child>()
                .ForMember(d => d.ParentId, o => o.Ignore())
                .ForMember(d => d.InstitutionId, o => o.Ignore());
        }
        #endregion
    }
}
