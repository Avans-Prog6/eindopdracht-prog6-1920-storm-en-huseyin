using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Models
{
	public class BookingProcess
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public int BookingId { get; set; }
		public Booking Booking { get; set; }

		public int ClientInfoId { get; set; }
		public ClientInfo ClientInfo { get; set; }

		public List<Animal> Animals { get; set; }
		public List<Accessories> Accessories { get; set; }
		
		[NotMapped]
		public List<Discounts> Discounts { get; set; }
		
		[DisplayFormat(DataFormatString = "{0:n} €")]
		public double TotalPrice { get; set; }
		public double TotalDiscount { get; set; }
		public bool BookingIsConfirmed { get; set; }
	}
}
