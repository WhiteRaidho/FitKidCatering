using AutoMapper;
using FitKidCateringApp.Models.Orders;
using FitKidCateringApp.Services.Children;
using FitKidCateringApp.Services.Offers;
using FitKidCateringApp.Services.Orders;
using FitKidCateringApp.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Controllers.Orders
{
    [Authorize]
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        protected IMapper Mapper { get; }
        protected OrdersService Orders { get; }
        protected ChildrenService Children { get; }
        protected OffersService Offers { get; }

        #region OrdersController()
        public OrdersController(
            IMapper mapper,
            OrdersService orders,
            ChildrenService children,
            OffersService offers
        )
        {
            Mapper = mapper;
            Orders = orders;
            Children = children;
            Offers = offers;
        }
        #endregion

        #region GetByChildId()
        [HttpGet("{childPublicId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<OrderViewModel>> GetById(Guid childPublicId)
        {
            var child = Children.GetById(childPublicId);
            if (child == null) return NotFound();
            //TODO check if user can manage child offers

            OrderViewModel result;

            var order = Orders.GetByChildId(child.Id);
            result = new OrderViewModel()
            {
                ChildPublicId = childPublicId,
                Offers = new List<Guid>()
            };
            if (order == null)
                result.Offers = order.Offers.Select(x => Offers.GetById(x).PublicId).ToList();

            return Ok(result);
        }
        #endregion

        #region ChangeOrder()
        [HttpPost("{childPublicId}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> ChangeOrder(Guid childPublicId, [FromBody]List<Guid> offers)
        {
            var child = Children.GetById(childPublicId);
            if (child == null) return NotFound();

            var order = Orders.GetByChildId(child.Id);
            var offerIds = offers.Select(x => Offers.GetById(x).Id).ToList();
            if(order == null)
            {
                order = new Order()
                {
                    ChildId = child.Id,
                    Offers = offerIds
                };

                Orders.Create(order);
            }
            else
            {
                order.Offers = offerIds;
                Orders.Update(order);
            }

            return Accepted();
        }
        #endregion
    }
}
