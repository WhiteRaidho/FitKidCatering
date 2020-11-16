using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FitKidCateringApp.Models.Institutions;
using FitKidCateringApp.Services.Core;
using FitKidCateringApp.Services.Institutions;
using FitKidCateringApp.ViewModels.Institutions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitKidCateringApp.Controllers.Institutions
{
    [Authorize]
    [ApiController]
    [Route("api/institutions")]
    public class InstitutionsController : ControllerBase
    {
        protected IMapper Mapper { get; }
   
        protected InstitutionsService Institutions { get; }

        protected CoreUserService Users { get; }

        public InstitutionsController(IMapper mapper,InstitutionsService institutions, CoreUserService users)
        {
            Mapper = mapper;
            Institutions = institutions;
            Users = users;
        }

        #region GetById()
        [AllowAnonymous]
        [HttpGet("{publicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<InstitutionListItemModel>> GetById(Guid publicId)
        {
            var institution = Institutions.GetById(publicId);
            if (institution == null) return NotFound();

            var result = Mapper.Map<InstitutionListItemModel>(institution);
            return Ok(result);
        }
        #endregion

        #region GetInstitutions()
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<InstitutionListItemModel>>> GetInstitutions()
        {
            var institutions = Institutions.GetList();
            var result = Mapper.Map<IEnumerable<InstitutionListItemModel>>(institutions);
            return Ok(result);
        }
        #endregion

        #region Create()
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody]InstitutionFormModel model)
        {
            Guid formPublicId = model.OwnerPublicId;
            var entity = Mapper.Map<Institution>(model);
            var user = Users.GetCoreUser(formPublicId);
            entity.OwnerId = user.Id;
            entity.Owner = user;
            entity = Institutions.Create(entity);
            var newentity = Institutions.GetInstitution(entity.Id);
            return CreatedAtAction(nameof(GetById), new { publicId = entity.PublicId }, Mapper.Map<InstitutionListItemModel>(entity));
        }
        #endregion

        #region Edit()
        [HttpPut("{publicId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create(Guid publicId, [FromBody]InstitutionFormModel model)
        {
            Guid formPublicId = model.OwnerPublicId;
            var entity = Institutions.GetById(publicId);
            if (entity == null) return NotFound();
            var user = Users.GetCoreUser(formPublicId);
            entity.OwnerId = user.Id;
            entity.Owner = user;

            entity = Mapper.Map(model, entity);
            entity = Institutions.Update(entity);

            return Accepted();
        }
        #endregion

        #region Remove()
        [HttpDelete("{publicId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Remove(Guid publicId)
        {
            var entity = Institutions.GetById(publicId);

            if (entity == null) return NotFound();

            Institutions.Remove(entity);
            return Accepted();
        }
        #endregion
    }


}