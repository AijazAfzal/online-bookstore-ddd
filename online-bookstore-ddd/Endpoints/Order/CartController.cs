using MediatR;
using Microsoft.AspNetCore.Mvc;
using online_bookstore_ddd.MediatR.Queries;
using online_bookstore_ddd.Models.DTOs;
namespace online_bookstore_ddd.Endpoints.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
                _mediator = mediator;
        }
        [HttpGet("GetCustomerCartDetailsAsync")] 
        [ProducesResponseType(typeof(CartDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<CartDTO>> GetCustomerCartDetailsAsync([FromBody] GetCustomerCartDetailsQuery query)
        {
            return await _mediator.Send(query);  
        }
    }
}
