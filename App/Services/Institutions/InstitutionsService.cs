using AutoMapper;
using FitKidCateringApp.Models;
using FitKidCateringApp.Models.Institutions;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Services.Institutions
{
    public class InstitutionsService : BaseService
    {
        #region InstitutionsService()
        public InstitutionsService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {   
        }
        #endregion

        public Institution GetInstitution(long id)
        {
            var Institution = Context.Institutions.FirstOrDefault(u => u.Id == id);
            return Institution;
        }

        #region GetById()
        public Institution GetById(Guid publicId)
        {
            return Context.Institutions.FirstOrDefault(x => x.PublicId == publicId);
        }
        #endregion

        #region GetList()
        public List<Institution> GetList()
        {
            return Context.Institutions.ToList();
        }

        #endregion
    }
}
