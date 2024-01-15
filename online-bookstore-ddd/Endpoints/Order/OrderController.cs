using MediatR;
using Microsoft.AspNetCore.Mvc;
using online_bookstore_ddd.MediatR.Commands;
using online_bookstore_ddd.Models;
namespace online_bookstore_ddd.Endpoints.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator; 
        }
        [HttpPost("PlaceOrderAsyncAsync")]
        [ProducesResponseType(typeof(ResponseMessage), StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponseMessage>> PlaceOrderAsyncAsync([FromBody] PlaceOrderCommand command)
        {
            return await _mediator.Send(command); 
        }
        [HttpDelete("CancelOrderAsyncAsync")] 
        [ProducesResponseType(typeof(ResponseMessage), StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponseMessage>> CancelOrderAsyncAsync([FromBody] CancelOrderCommand command)
        {
            return await _mediator.Send(command);  
        }
    }
}
