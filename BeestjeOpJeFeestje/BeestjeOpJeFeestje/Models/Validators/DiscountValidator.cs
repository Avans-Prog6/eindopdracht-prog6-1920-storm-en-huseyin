using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Models.Validators
{
    public class DiscountValidator
    {
        public int GetAlphabeticalDiscount(List<Animal> animals)
        {
            if (animals == null) return 0;

            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int totalDiscount = 0;
            foreach (Animal animal in animals)
            {
                string name = animal.Name.ToUpper();
                for (int i = 0; i < name.Length; i++)
                {
                    if (name[i] == alphabet[i])
                    {
                        totalDiscount += 2;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return totalDiscount;
        }

        public bool BookingIsMondayOrTuesday(in DateTime bookingDate)
        {
            return bookingDate.DayOfWeek == DayOfWeek.Monday || bookingDate.DayOfWeek == DayOfWeek.Tuesday;
        }

        public int ChanceOnDuckDiscount(List<Animal> animals)
        {
            if (!AnimalsHasDuck(animals))
                return 0;

            Random random = new Random();
            return random.Next(0, 6) == 1 ? 50 : 0;
        }

        public bool AnimalsHasDuck(List<Animal> animals)
        {
            return animals.Any(e => e.Name == "Eend");
        }

        public bool HasThreeSameTypeAnimals(List<Animal> animals)
        {
            if (animals.Count < 3)
            {
                return false;
            }

            int desertType = 0;
            int snowType = 0;
            int farmType = 0;
            int jungleType = 0;

            foreach (Animal animal in animals)
            {
                switch (animal.Type)
                {
                    case AnimalTypes.Woestijn:
                        desertType++;
                        break;
                    case AnimalTypes.Sneeuw:
                        snowType++;
                        break;
                    case AnimalTypes.Boerderij:
                        farmType++;
                        break;
                    case AnimalTypes.Jungle:
                        jungleType++;
                        break;
                }
            }

            return desertType >= 3 || snowType >= 3 || farmType >= 3 || jungleType >= 3;
        }

        public int GetMaximumPercentage(int percentage, int max)
        {
            return Math.Clamp(percentage, 0, max);
        }
	}
}
