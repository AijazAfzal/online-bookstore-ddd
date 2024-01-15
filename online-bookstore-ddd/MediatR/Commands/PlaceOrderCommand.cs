using Bookstore.Domain.ValueObjects;
using MediatR;
using online_bookstore_ddd.Models; 
namespace online_bookstore_ddd.MediatR.Commands
{
    public class PlaceOrderCommand : IRequest<ResponseMessage>
    {
        public int CustomerId { get; set; }

        public Address address { get; set; }
    }
}
