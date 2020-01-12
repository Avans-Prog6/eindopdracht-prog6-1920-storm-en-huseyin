using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeestjeOpJeFeestje.Models.CustomValidation.AnimalSelection;

namespace BeestjeOpJeFeestje.Models
{
	public class BookingProcess
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[NotMapped]
		public Booking Booking { get; set; }

		public int ClientInfoId { get; set; }
		public ClientInfo ClientInfo { get; set; }

		public List<BookingProcessAnimal> BookingProcessAnimals { get; set; }
		public List<BookingProcessAccessories> BookingProcessAccessories { get; set; }
		
		public DateTime DateTime { get; set; }

		[NotMapped]
		// [AnimalSelection]
		public List<Animal> Animals { get; set; }
		[NotMapped]
		public List<Accessories> Accessories { get; set; }
		[NotMapped]
		public List<Discounts> Discounts { get; set; }
		
		[DisplayFormat(DataFormatString = "{0:n} €")]
		public double TotalPrice { get; set; }
		public double TotalDiscount { get; set; }
	}
}
