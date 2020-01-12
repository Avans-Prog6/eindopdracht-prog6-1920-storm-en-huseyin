using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Models
{
	public class Discounts
	{
		public string Name { get; set; }

		public double Discount { get;  set; }
	}
}
