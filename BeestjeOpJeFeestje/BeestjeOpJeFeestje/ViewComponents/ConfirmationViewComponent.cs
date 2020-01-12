using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using BeestjeOpJeFeestje.Models.Validators;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.ViewComponents
{
	public class ConfirmationViewComponent : ViewComponent
	{
		private readonly IRepository<Animal> _animalRepository;
		private readonly IRepository<Accessories> _accessoriesRepository;
		private readonly IRepository<ClientInfo> _clientInfoRepository;
		private readonly IRepository<BookingProcess> _bookingProcessRepository;
		private readonly IRepository<Booking> _bookingRepository;

		public ConfirmationViewComponent(IRepository<BookingProcess> bookingProcessRepository,
			IRepository<Booking> bookingRepository,
			IRepository<Animal> animalRepository,
			IRepository<Accessories> accessoriesRepository,
			IRepository<ClientInfo> clientInfoRepository)
		{
			_animalRepository = animalRepository;
			_accessoriesRepository = accessoriesRepository;
			_clientInfoRepository = clientInfoRepository;
			_bookingProcessRepository = bookingProcessRepository;
			_bookingRepository = bookingRepository;
		}

		public async Task<IViewComponentResult> InvokeAsync(BookingProcess data)
		{
			List<Animal> animals = await GetAnimals(data.Animals.Select(e => e.ID).ToArray());
			List<Accessories> accessories = new List<Accessories>();
			List<Discounts> discounts = new List<Discounts>();

			DiscountValidator discountValidator = new DiscountValidator();

            if (data.Accessories != null)
			{
				accessories = await GetAccessories(data.Accessories.Select(e => e.ID).ToArray());
			}

			data.Animals = animals;
			data.Accessories = accessories;

			#region Discount

			int totalDiscountPercentage = 0;
			if (discountValidator.HasThreeSameTypeAnimals(animals))
            {
                totalDiscountPercentage += 10;
				discounts.Add(new Discounts()
				{
					Name = "3 Types",
					Discount = 10
				});
			}

            int duckDiscount = discountValidator.ChanceOnDuckDiscount(animals);

			if (duckDiscount > 0)
			{
				totalDiscountPercentage += duckDiscount;
				discounts.Add(new Discounts()
					{
						Name = "Eend",
						Discount = duckDiscount
				}
				);
			}

			if (discountValidator.BookingIsMondayOrTuesday(data.Booking.Date))
			{
				totalDiscountPercentage += 15;
				discounts.Add(new Discounts()
				{
					Name = data.Booking.Date.DayOfWeek.ToString(),
					Discount = 15
				});
			}

			int alphabetDiscount = discountValidator.GetAlphabeticalDiscount(animals);
			if (alphabetDiscount > 0)
			{
				totalDiscountPercentage += alphabetDiscount;
				discounts.Add(new Discounts()
				{
					Name = "Alfabet",
					Discount = alphabetDiscount
				});
			}

			totalDiscountPercentage = discountValidator.GetMaximumPercentage(totalDiscountPercentage, 60);
			data.TotalDiscount = totalDiscountPercentage;

			#endregion

			#region TotalPrice

			double totalPrice = 0.0;
			foreach (Animal animal in animals)
			{
				totalPrice += animal.Price;
			}

			foreach (Accessories accessoriese in accessories)
			{
				totalPrice += accessoriese.Price;
			}

			totalPrice = totalPrice / 100 * (100 - totalDiscountPercentage);

			#endregion

			data.TotalPrice = totalPrice;
			data.TotalDiscount = totalDiscountPercentage;
			data.Discounts = discounts;

			#region SaveData

			// Save or Get Client Id
			await _clientInfoRepository.Create(data.ClientInfo);
			data.ClientInfoId = data.ClientInfo.ID;
			data.DateTime = data.Booking.Date;

			data.BookingProcessAnimals = new List<BookingProcessAnimal>();
			data.BookingProcessAccessories = new List<BookingProcessAccessories>();
			foreach (Animal animal in animals)
			{
				data.BookingProcessAnimals.Add(new BookingProcessAnimal()
				{
					BookingProcessId = data.ID,
					AnimalId = animal.ID
				});
			}

			foreach (Accessories a in accessories)
			{
				data.BookingProcessAccessories.Add(new BookingProcessAccessories()
				{
					BookingProcessId = data.ID,
					AccessoriesId = a.ID
				});
			}

			await _bookingProcessRepository.Create(data);
			#endregion

			return View(data);
		}

		

        private async Task<List<Accessories>> GetAccessories(params int[] accessoriesId)
		{
			List<Accessories> list = new List<Accessories>();
			List<Accessories> accessories = await _accessoriesRepository.Find(accessoriesId);

			foreach (Accessories a in accessories)
			{
				list.Add(a);
			}

			return list;
		}

        private async Task<List<Animal>> GetAnimals(params int[] animalId)
		{
			List<Animal> list = new List<Animal>();
			List<Animal> animals = await _animalRepository.Find(animalId);

			foreach (Animal animal in animals)
			{
				list.Add(animal);
			}

			return list;
		}
	}
}