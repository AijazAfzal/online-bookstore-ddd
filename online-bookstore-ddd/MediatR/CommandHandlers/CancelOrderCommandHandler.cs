using Bookstore.Domain.Interfaces;
using Bookstore.Domain.Services;
using MediatR;
using online_bookstore_ddd.MediatR.Commands;
using online_bookstore_ddd.Models;
namespace online_bookstore_ddd.MediatR.CommandHandlers
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, ResponseMessage>
    {
        private readonly IAppLogger<PlaceOrderCommandHandler> _logger;
        private readonly IOrderService _orderService;  
        public CancelOrderCommandHandler(IAppLogger<PlaceOrderCommandHandler> logger,IOrderService orderService)
        {
                _logger = logger;
                _orderService = orderService; 
        }
        public async Task<ResponseMessage> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _orderService.CancelOrderAsync(request.CustomerId);
            }
            catch (Exception ex) 
            {
                _logger.LogWarning($"error occured {{0}}", nameof(CancelOrderCommandHandler)); 
                throw;
            }
        }
    }
}
