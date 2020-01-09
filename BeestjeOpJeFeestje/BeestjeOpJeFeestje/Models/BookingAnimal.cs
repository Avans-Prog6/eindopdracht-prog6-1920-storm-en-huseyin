using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Models
{
	public class BookingAnimal
	{
		[Key]
		public int AnimalId { get; set; }
		public Animal Animal { get; set; }

		[Key]
		public int BookingId { get; set; }
		public Booking Booking { get; set; }
	}
}
