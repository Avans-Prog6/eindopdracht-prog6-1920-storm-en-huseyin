using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Models
{
	public class Booking
	{
		
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }


		public Booking()
		{

		}

		public List<BookingAnimal> BookingAnimals { get; set; }
	}
}
