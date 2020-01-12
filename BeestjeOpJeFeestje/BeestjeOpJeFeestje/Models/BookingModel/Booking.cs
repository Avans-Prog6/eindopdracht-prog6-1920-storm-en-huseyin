using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeestjeOpJeFeestje.Models.CustomValidation;

namespace BeestjeOpJeFeestje.Models
{
	public class Booking
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[DateIsNotInThePast]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
		
		[DisplayFormat(DataFormatString = "{0:n} €"), Display(Name = "Total Price")]
        public double TotalPrice { get; set; }

        [Display(Name = "Booked Animals")]
        public List<BookingAnimal> BookingAnimals { get; set; }
        [Display(Name = "Booked Accessories")]
		public List<BookingAccessories> BookingAccessories { get; set; }

        [Display(Name = "Client ID")]
		public int ClientInfoId { get; set; }
        [Display(Name = "Client")]
		public ClientInfo ClientInfo { get; set; }

		[NotMapped] public BookingState BookingState { get; set; } = BookingState.Animals;

		public Booking()
		{
		}


	}
}