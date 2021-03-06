﻿using AutoMapper;
using FitKidCateringApp.Models;
using FitKidCateringApp.Models.Offers;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Services.Offers
{
    public class OffersService : BaseService
    {
        #region OffersService()
        public OffersService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        #endregion

        #region GetList()
        public List<Offer> GetList()
        {
            return Context.Offers
                .ToList();
        }
        #endregion

        #region GetById()
        public Offer GetById(long id)
        {
            return Context.Offers.FirstOrDefault(x => x.Id == id);
        }

        public Offer GetById(Guid publicId)
        {
            return Context.Offers.FirstOrDefault(x => x.PublicId == publicId);
        } 
        #endregion
    }
}
