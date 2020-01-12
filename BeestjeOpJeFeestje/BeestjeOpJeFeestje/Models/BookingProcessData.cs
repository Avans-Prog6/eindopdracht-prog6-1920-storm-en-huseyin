using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Models;

namespace BeestjeOpJeFeestje.Models
{
	public class BookingProcessData
	{
		public Booking Booking { get; set; }
		public ClientInfo ClientInfo { get; set; }
		public List<Animal> Animals { get; set; }
		public List<Accessories> Accessories { get; set; }
		public List<Discounts> Discounts { get; set; }
		
		[DisplayFormat(DataFormatString = "{0:n} €")]
		public double TotalPrice { get; set; }
		public double TotalDiscount;
	}
}
