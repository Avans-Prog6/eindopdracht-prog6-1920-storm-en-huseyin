using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
	}
}
