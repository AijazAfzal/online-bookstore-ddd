using Bookstore.Domain.Entities;
using Bookstore.Domain.Entities.CartAggregate;
using Bookstore.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
namespace Bookstore.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {          
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Book> Books { get; set; }   
    }
}
