using AutoMapper;
using FitKidCateringApp.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Models.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() : base()
        {
            UserProfile();
        }

        #region UserProfile()
        protected void UserProfile()
        {
            CreateMap<RegisterViewModel, CoreUser>()
                .ForMember(d => d.PasswordHash, o => o.Ignore());

            CreateMap<CoreUser, UserListItemModel>();
        } 
        #endregion
    }
}
