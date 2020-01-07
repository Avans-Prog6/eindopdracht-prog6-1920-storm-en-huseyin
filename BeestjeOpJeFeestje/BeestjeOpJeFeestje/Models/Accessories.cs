using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeestjeOpJeFeestje.Models
{
	public class Accessories
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string Name { get; private set; }
		public double Price { get; private set; }
		public string PicturePath { get; private set; }
		public Accessories(string name, double price, string picturePath = "")
		{
			Name = name;
			Price = price;
			PicturePath = picturePath;
		}
		
		public List<AnimalAccessories> AnimalAccessories { get; set; }
	}
}
