using Bookstore.Domain.Interfaces;
using Bookstore.Domain.Services;
using MediatR;
using online_bookstore_ddd.MediatR.Queries;
using online_bookstore_ddd.Models.DTOs;
namespace online_bookstore_ddd.MediatR.QueryHandlers
{
    public class GetCustomerCartDetailsQueryHandler : IRequestHandler<GetCustomerCartDetailsQuery, CartDTO>
    {
        private readonly ICartService _cartService;
        private readonly IAppLogger<GetCustomerCartDetailsQueryHandler> _logger;

        public GetCustomerCartDetailsQueryHandler(ICartService cartService, IAppLogger<GetCustomerCartDetailsQueryHandler> logger)
        {
                _logger = logger;
                _cartService = cartService; 
        }
        public async Task<CartDTO> Handle(GetCustomerCartDetailsQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                return await _cartService.GetCustomerCartDetailsAsync(request.CustomerId); 
            }
            catch (Exception ex) 
            {
                _logger.LogWarning($"error occured {{0}}", nameof(GetCustomerCartDetailsQueryHandler));
                throw;
            }
        }
    }
}
