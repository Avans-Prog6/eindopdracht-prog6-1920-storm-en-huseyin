using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Models
{
	public class BookingAccessories
	{
		[Key]
		public int AccessoriesId { get; set; }
		public Accessories Accessories { get; set; }

		[Key]
		public int BookingId { get; set; }
		public Booking Booking { get; set; }
	}
}
