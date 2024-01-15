using MediatR;
using online_bookstore_ddd.Models.DTOs;
namespace online_bookstore_ddd.MediatR.Queries
{
    public class GetCustomerCartDetailsQuery : IRequest<CartDTO>
    {
        public int CustomerId { get; set; } 
    }
}
