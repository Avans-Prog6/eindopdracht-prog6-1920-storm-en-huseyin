using System.Collections.Generic;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.ViewComponents
{
	public class AnimalSelectionViewComponent : ViewComponent
	{
		private readonly AnimalDbRepository _repository;

		public AnimalSelectionViewComponent(IRepository<Animal> repository)
		{
			_repository = (AnimalDbRepository)repository;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			List<Animal> animals = await _repository.GetAll();
			return View(animals);
		}
	}
}
