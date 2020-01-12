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
		private readonly IRepository<BookingProcess> _bookingProcessRepository;

		public BookingsController(IRepository<Booking> bookingRepository, IRepository<Animal> animalRepository, IRepository<BookingProcess> bookingProcessRepository)
		{
			_bookingRepository = bookingRepository;
			_animalRepository = animalRepository;
			_bookingProcessRepository = bookingProcessRepository;
		}

		// GET: Bookings/Edit/5
		public async Task<IActionResult> AnimalSelection(Booking booking)
		{
			Booking foundBooking = await ((BookingDBRepository) _bookingRepository).GetFromDate(booking.Date);
			if (foundBooking != null)
			{
				booking = foundBooking;
			}

			BookingProcess bookingProcess = new BookingProcess()
			{
				Booking = booking
			};

			return View(bookingProcess);
		}



		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AnimalsSelected(BookingProcess data)
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

		public async Task<IActionResult> AccessoriesSelected(BookingProcess data)
		{
			List<Accessories> accessories = new List<Accessories>();
			foreach (Accessories dataAccessory in data.Accessories)
			{
				if (dataAccessory.BookingIsSelected)
				{
					accessories.Add(dataAccessory);
				}
			}

			data.Accessories = accessories;
			data.Booking.BookingState = BookingState.Details;
			return View("AnimalSelection", data);
		}

		public IActionResult PersonalInformation(BookingProcess data)
		{
			data.Booking.BookingState = BookingState.Confirmation;
			return View("AnimalSelection", data);
		}

		public async Task<IActionResult> ConfirmBooking(BookingProcess data)
		{
			BookingProcess bookingProcess = await _bookingProcessRepository.Get(data.ID);
			Booking booking = new Booking
			{
				BookingAccessories = new List<BookingAccessories>(),
				BookingAnimals = new List<BookingAnimal>(),
				ClientInfoId = bookingProcess.ClientInfoId,
				TotalPrice = bookingProcess.TotalPrice,
				Date = bookingProcess.DateTime
			};

			foreach (BookingProcessAccessories bpAccessories in bookingProcess.BookingProcessAccessories)
			{
				booking.BookingAccessories.Add(new BookingAccessories()
				{
					AccessoriesId = bpAccessories.AccessoriesId
				});
			}
			foreach (BookingProcessAnimal bpAnimal in bookingProcess.BookingProcessAnimals)
			{
				booking.BookingAnimals.Add(new BookingAnimal()
				{
					AnimalId = bpAnimal.AnimalId
				});
			}

			await _bookingRepository.Create(booking);
			return RedirectToActionPermanent(nameof(HomeController.Index), "Home");
		}
	}
}