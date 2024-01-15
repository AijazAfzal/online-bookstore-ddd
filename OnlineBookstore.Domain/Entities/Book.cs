using Bookstore.Domain.SeedWork;
using Bookstore.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Domain.Entities
{
    public class Book : IAggregateRoot 
    {
        [Key]
        public int Id { get; private set; }

        public string Name { get; private set; }

        public AuthorName authorName { get; private set; } 

        public Genre Genre { get; private set; } 

        public decimal Price { get; private set; }

        public Book(string name, AuthorName authorname, Genre genre, decimal price)
        {
            Name = name;
            authorName = authorname; 
            Genre = genre; 
            Price = price;
        }
    }
}
