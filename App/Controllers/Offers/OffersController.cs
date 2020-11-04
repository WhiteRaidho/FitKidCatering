using AutoMapper;
using FitKidCateringApp.Attributes;
using FitKidCateringApp.Extensions;
using FitKidCateringApp.Helpers;
using FitKidCateringApp.Models.Offers;
using FitKidCateringApp.Services.Offers;
using FitKidCateringApp.ViewModels.Offers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Controllers.Offers
{
    [Authorize]
    [ApiController]
    [Route("api/offers")]
    public class OffersController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected OffersService Offers { get; }

        #region OffersController()
        public OffersController(IMapper mapper, OffersService offers)
        {
            Mapper = mapper;
            Offers = offers;
        }
        #endregion

        #region GetById()
        [AllowAnonymous]
        [HttpGet("{publicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OfferListItemModel>> GetById(Guid publicId)
        {
            var item = Offers.GetById(publicId);

            if (item == null) return NotFound();

            var result = Mapper.Map<OfferListItemModel>(item);
            return Ok(result);
        }
        #endregion

        #region GetOffers()
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<OfferListItemModel>>> GetOffers()
        {
            var offers = Offers.GetList();
            var result = Mapper.Map<IEnumerable<OfferListItemModel>>(offers);
            return Ok(result);
        }
        #endregion

        #region GetCurrentOffers()
        [AllowAnonymous]
        [HttpGet("current")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<OfferListItemModel>>> GetCurrentOffers()
        {
            var weekStart = DateTime.Now.StartOfWeek();
            var weekEnd = DateTime.Now.EndOfWeek();

            var offers = Offers.GetList(weekStart, weekEnd);
            var result = Mapper.Map<IEnumerable<OfferListItemModel>>(offers);
            return Ok(result);
        }
        #endregion

        #region Create()
        [RequireAll(StandardPermissions.CateringEmployee)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create([FromBody]OfferFormModel model)
        {
            var entity = Mapper.Map<Offer>(model);
            entity = Offers.Create(entity);
            return CreatedAtAction(nameof(GetById), new { publicId = entity.PublicId }, Mapper.Map<OfferListItemModel>(entity));
        }
        #endregion

        #region Edit()
        [RequireAll(StandardPermissions.CateringEmployee)]
        [HttpPut("{publicId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Create(Guid publicId, [FromBody]OfferFormModel model)
        {
            var entity = Offers.GetById(publicId);

            if (entity == null) return NotFound();

            entity = Mapper.Map(model, entity);
            entity = Offers.Update(entity);

            return Accepted();
        }
        #endregion

        #region Remove()
        [RequireAll(StandardPermissions.CateringEmployee)]
        [HttpDelete("{publicId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Remove(Guid publicId)
        {
            var entity = Offers.GetById(publicId);

            if (entity == null) return NotFound();

            Offers.Remove(entity);
            return Accepted();
        }
        #endregion
    }
}
