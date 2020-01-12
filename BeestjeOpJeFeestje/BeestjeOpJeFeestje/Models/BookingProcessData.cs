using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;

namespace BeestjeOpJeFeestje.Models
{
	public class BookingProcessData
	{
		public Booking Booking { get; set; }
		public List<Animal> Animals { get; set; }
		public List<Accessories> Accessories { get; set; }
	}
}
