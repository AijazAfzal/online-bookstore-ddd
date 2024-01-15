using MediatR;
using online_bookstore_ddd.Models;
namespace online_bookstore_ddd.MediatR.Commands
{
    public class CancelOrderCommand : IRequest<ResponseMessage>
    {
        public int CustomerId { get; set; }
    }
}
