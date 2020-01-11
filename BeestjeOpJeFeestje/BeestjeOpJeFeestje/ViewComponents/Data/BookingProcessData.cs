using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;

namespace BeestjeOpJeFeestje.ViewComponents.Data
{
	public class BookingProcessData
	{
		public Booking Booking { get; set; }
		public List<Animal> Animals { get; set; }
	}
}
