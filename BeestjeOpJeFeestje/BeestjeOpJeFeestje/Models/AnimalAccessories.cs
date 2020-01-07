using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Models
{
	public class AnimalAccessories
	{
		[Key]
		public int AnimalId { get; set; }
		public Animal Animal { get; set; }

		[Key]
		public int AccessoriesId { get; set; }
		public Accessories Accessories { get; set; }
	}
}
