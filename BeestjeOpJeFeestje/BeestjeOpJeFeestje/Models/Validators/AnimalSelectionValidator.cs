using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Controllers;

namespace BeestjeOpJeFeestje.Models.Validators
{
	public class AnimalSelectionValidator
	{
		public bool FarmAnimalHasNoLionOrIceBear(List<Animal> animals)
		{
			if (!HasFarmAnimal(animals))
			{
				return true;
			}

			foreach (Animal a in animals)
			{
				if (a.Name == "Leeuw" || a.Name == "Ijsbeer")
				{
					return false;
				}
			}

			return true;
		}

		public bool HasFarmAnimal(List<Animal> animals)
		{
			foreach (Animal animal in animals)
			{
				if (animal.Type == AnimalTypes.Boerderij)
				{
					return true;
				}
			}

			return false;
		}

		public bool PenguinIsHiredInWeekend(List<Animal> animals, DateTime time)
		{
			foreach (Animal animal in animals)
			{
				if (animal.Name == "Pinguïn" && (time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday))
				{
					return true;
				}
			}

			return false;
		}

		public bool DesertAnimalIsHiredInWinter(List<Animal> animals, DateTime time)
		{
			foreach (Animal animal in animals)
			{
				if (animal.Type == AnimalTypes.Woestijn &&
				    (time.Month == 10 || time.Month == 11 || time.Month == 12 || time.Month == 1 || time.Month == 2))
				{
					return true;
				}
			}

			return false;
		}

		public bool SnowAnimalIsHiredForSummer(List<Animal> animals, DateTime time)
		{
			foreach (Animal animal in animals)
			{
				if (animal.Type == AnimalTypes.Sneeuw &&
				    (time.Month == 6 || time.Month == 7 || time.Month == 8))
				{
					return true;
				}
			}

			return false;
		}

		public bool IsAnimalAlreadyBooked(List<BookingAnimal> bookingBookingAnimals, int animalId)
		{
			foreach (BookingAnimal bookingAnimal in bookingBookingAnimals)
			{
				if (bookingAnimal.AnimalId == animalId)
				{
					return true;
				}
			}

			return false;
		}

		public bool IsAnAnimalSelected(List<Animal> selectedAnimals)
		{
			return selectedAnimals.Count > 0;
		}
	}
}
