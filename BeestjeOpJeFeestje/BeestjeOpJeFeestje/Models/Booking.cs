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

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		[DataType(DataType.Date), DateIsNotBooked]
		public DateTime Date { get; set; }


		public Booking()
		{

		}

		public List<BookingAnimal> BookingAnimals { get; set; }
	}
}
