using AutoMapper;
using FitKidCateringApp.Models;
using FitKidCateringApp.Models.Children;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Services.Children
{
    public class ChildrenService : BaseService
    {
        #region ChildrenService()
        public ChildrenService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
        #endregion

        #region GetChild()
        public Child GetChild(long id)
        {
            return Context.Children.FirstOrDefault(u => u.Id == id);
        }
        #endregion

        #region GetById()
        public Child GetById(Guid publicId)
        {
            return Context.Children.Include(x => x.Parent).Include(x => x.Institution).FirstOrDefault(x => x.PublicId == publicId);
        }
        #endregion

        #region GetList()
        public List<Child> GetList()
        {
            return Context.Children.Include(x => x.Parent).Include(x => x.Institution).ToList();
        }
        #endregion

        #region GetMyChild()
        public List<Child> GetMyChild(long id)
        {

            return Context.Children.Where(x => x.ParentId == id).Cast<Child>().ToList();
            
        }
        #endregion

    }
}
