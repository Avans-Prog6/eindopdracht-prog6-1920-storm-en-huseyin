using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;
using BeestjeOpJeFeestje.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeestjeOpJeFeestje.ViewComponents
{
	public class ConfirmationViewComponent : ViewComponent
	{
		private readonly IRepository<Animal> _animalRepository;
		private readonly IRepository<Accessories> _accessoriesRepository;
		private readonly IRepository<ClientInfo> _clientInfoRepository;
		private readonly IRepository<BookingProcess> _bookingProcessRepository;

		public ConfirmationViewComponent(IRepository<BookingProcess> bookingProcessRepository,
			IRepository<Animal> animalRepository,
			IRepository<Accessories> accessoriesRepository,
			IRepository<ClientInfo> clientInfoRepository)
		{
			_animalRepository = animalRepository;
			_accessoriesRepository = accessoriesRepository;
			_clientInfoRepository = clientInfoRepository;
			_bookingProcessRepository = bookingProcessRepository;
		}

		public async Task<IViewComponentResult> InvokeAsync(BookingProcess data)
		{
			List<Animal> animals = await GetAnimals(data.Animals.Select(e => e.ID).ToArray());
			List<Accessories> accessories = new List<Accessories>();
			List<Discounts> discounts = new List<Discounts>();

			if (data.Accessories != null)
			{
				accessories = await GetAccessories(data.Accessories.Select(e => e.ID).ToArray());;
			}

			data.Animals = animals;
			data.Accessories = accessories;

			#region Discount

			int totalDiscountPercentage = 0;
			if (HasThreeSameTypeAnimals(animals))
			{
				totalDiscountPercentage += 10;
				discounts.Add(new Discounts()
				{
					Name = "3 Types",
					Discount = 10
				});
			}

			if (ChanceOnDuckDiscount(animals))
			{
				totalDiscountPercentage += 50;
				discounts.Add(new Discounts()
					{
						Name = "Eend",
						Discount = 50
					}
				);
			}

			if (BookingIsMondayOrTuesday(data.Booking.Date))
			{
				totalDiscountPercentage += 15;
				discounts.Add(new Discounts()
				{
					Name = data.Booking.Date.DayOfWeek.ToString(),
					Discount = 15
				});
			}

			int alphabetDiscount = GetAlphabeticalDiscount(animals);
			if (alphabetDiscount > 0)
			{
				totalDiscountPercentage += alphabetDiscount;
				discounts.Add(new Discounts()
				{
					Name = "Alfabet",
					Discount = alphabetDiscount
				});
			}

			totalDiscountPercentage = GetMaximumPercentage(totalDiscountPercentage, 60);
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
			ClientInfo clientInfo = await ((ClientInfoDBRepository) _clientInfoRepository).Find(data.ClientInfo.Email);
			if (clientInfo != null)
			{
				data.ClientInfo = clientInfo;
				data.ClientInfoId = clientInfo.ID;
			}
			else
			{
				await _clientInfoRepository.Create(data.ClientInfo);
				data.ClientInfoId = data.ClientInfo.ID;
			}

			await _bookingProcessRepository.Create(data);
			#endregion
			return View(data);
		}

		private int GetAlphabeticalDiscount(List<Animal> animals)
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

		private bool BookingIsMondayOrTuesday(in DateTime bookingDate)
		{
			return bookingDate.DayOfWeek == DayOfWeek.Monday || bookingDate.DayOfWeek == DayOfWeek.Tuesday;
		}

		private bool ChanceOnDuckDiscount(List<Animal> animals)
		{
			if (!AnimalsHasDuck(animals))
				return false;

			Random random = new Random();
			if (random.Next(0, 6) == 1)
			{
				return true;
			}

			return false;
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

		public async Task<List<Accessories>> GetAccessories(params int[] accessoriesId)
		{
			List<Accessories> list = new List<Accessories>();
			List<Accessories> accessories = await _accessoriesRepository.Find(accessoriesId);

			foreach (Accessories a in accessories)
			{
				list.Add(a);
			}

			return list;
		}

		public async Task<List<Animal>> GetAnimals(params int[] animalId)
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