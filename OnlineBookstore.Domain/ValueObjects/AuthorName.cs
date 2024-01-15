namespace Bookstore.Domain.ValueObjects
{
    public class AuthorName : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public AuthorName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
