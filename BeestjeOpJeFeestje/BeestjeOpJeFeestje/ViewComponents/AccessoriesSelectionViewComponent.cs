using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.ViewComponents
{
	public class AccessoriesSelectionViewComponent : ViewComponent
	{
		private readonly IRepository<Animal> _repository;

		public AccessoriesSelectionViewComponent(IRepository<Animal> repository)
		{
			_repository = repository;
		}

		public async Task<IViewComponentResult> InvokeAsync(BookingProcessData data)
		{
			List<Animal> animals = await _repository.Find(data.Animals.Select(e => e.ID).ToArray());

			List<Accessories> accessories = new List<Accessories>();
			foreach (Animal animal in animals)
			{
				foreach (Accessories animalAccessory in animal.Accessories)
				{
					accessories.Add(animalAccessory);
				}
			}

			data.Accessories = accessories;
				
			return View(data);
		}
	}
}