using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeestjeOpJeFeestje.Models
{
	public class Booking
	{
        //TODO: AnimalAccesories weghalen en Animals een 1 op meer relatie laten hebben met accesories! (Een beestje mag meerder accesories hebben)
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
