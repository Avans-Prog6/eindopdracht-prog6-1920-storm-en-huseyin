﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using BeestjeOpJeFeestje.ViewComponents.Data;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.ViewComponents
{
	public class AnimalSelectionViewComponent : ViewComponent
	{
		private readonly AnimalDbRepository _repository;

		public AnimalSelectionViewComponent(IRepository<Animal> repository)
		{
			_repository = (AnimalDbRepository) repository;
		}

		public async Task<IViewComponentResult> InvokeAsync(BookingProcessData data)
		{
			List<Animal> animals = await _repository.GetAll();

			if (data.Booking.BookingAnimals != null)
			{
				foreach (BookingAnimal bookingBookingAnimal in data.Booking.BookingAnimals)
				{
					foreach (Animal animal in animals)
					{
						if (animal.ID == bookingBookingAnimal.AnimalId)
						{
							animal.Booked = true;
						}
					}
				}
			}
				

			data.Animals = animals;
			return View(data);
		}
	}
}