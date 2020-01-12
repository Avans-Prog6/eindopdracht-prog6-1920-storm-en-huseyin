using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;

namespace BeestjeOpJeFeestje.Controllers
{
	public class BookingsController : Controller
	{
		private readonly IRepository<Booking> _bookingRepository;
		private readonly IRepository<Animal> _animalRepository;

		public BookingsController(IRepository<Booking> bookingRepository, IRepository<Animal> animalRepository)
		{
			_bookingRepository = bookingRepository;
			_animalRepository = animalRepository;
		}

		// GET: Bookings/Edit/5
		public async Task<IActionResult> AnimalSelection(Booking booking)
		{
			Booking foundBooking = await ((BookingDBRepository) _bookingRepository).GetFromDate(booking.Date);
			if (foundBooking != null)
			{
				booking = foundBooking;
			}

			BookingProcessData bookingProcessData = new BookingProcessData()
			{
				Booking = booking
			};

			return View(bookingProcessData);
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AnimalsSelected(BookingProcessData data)
		{
			Booking booking = await ((BookingDBRepository) _bookingRepository).GetFromDate(data.Booking.Date);
			List<Animal> selectedAnimals = new List<Animal>();
			foreach (Animal animal in data.Animals)
			{
				if (booking != null && animal.BookingIsSelected && animal.ID == booking.ID)
				{
					return RedirectToActionPermanent(nameof(AnimalSelection), booking);
				}

				if(animal.BookingIsSelected)
				{
					Animal dAnimal = await ((AnimalDbRepository) _animalRepository).Get(animal.ID);
					selectedAnimals.Add(dAnimal);
				}
			}


			data.Animals = selectedAnimals;
			data.Booking.BookingState = BookingState.Accessories;
			return View("AnimalSelection", data);
		}

		public async Task<IActionResult> AccessoriesSelected(BookingProcessData data)
		{
			return Ok(data);
		}
	}
}