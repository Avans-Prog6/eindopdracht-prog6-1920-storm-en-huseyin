using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace BeestjeOpJeFeestje.Models
{
    public static class AnimalTypes
    {
        public const string Woestijn = "Woestijn";
        public const string Jungle = "Jungle";
        public const string Sneeuw = "Sneeuw";
        public const string Boerderij = "Boerderij";

        public static bool Contains(string value)
        {
            return value.Equals(Woestijn) || value.Equals(Jungle) || value.Equals(Sneeuw) || value.Equals(Boerderij);
        }

        public static List<string> ToList()
        {
            List<string> animals = new List<string>
            {
                Woestijn,
                Jungle,
                Sneeuw,
                Boerderij
            };

            return animals;
        }

    }
}