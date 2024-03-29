﻿namespace Bookstore.Domain.ValueObjects
{
    public class Genre : ValueObject
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Genre(string name, string description)
        {
            Name = name;
            Description = description;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
        }
    }
}
