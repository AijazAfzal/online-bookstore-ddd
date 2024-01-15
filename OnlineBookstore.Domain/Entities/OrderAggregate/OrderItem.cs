using System.ComponentModel.DataAnnotations;
namespace Bookstore.Domain.Entities.OrderAggregate
{
    public class OrderItem
    {
        [Key]
        public int Id { get; private set; }

        public Book Book { get; private set; }

        public int Quantity { get; private set; }
        public OrderItem(Book book,int quantity)
        {
                Book = book;
                Quantity = quantity;
        }
    }
}
