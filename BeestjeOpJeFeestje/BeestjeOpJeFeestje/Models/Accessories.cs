using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using BeestjeOpJeFeestje.Models.CustomValidation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeestjeOpJeFeestje.Models
{
	public class Accessories
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
		public string Name { get; set; }

		[Required]
		[DisplayFormat(DataFormatString = "{0:n} €")]
		public double Price { get; set; }

        [Required]
        [Display(Name = "Picture")]
		public string PicturePath { get; set; }

		[Required]
	    [Display(Name = "Animal")]
        public int AnimalId{ get; set; }
        public Animal Animal { get; set; }


		[NotMapped]
		public bool BookingIsSelected { get; set; }
		public Accessories(string name, double price, int animalId, string picturePath = "")
		{
			Name = name;
			Price = price;
			PicturePath = picturePath;
            AnimalId = animalId;
        }

		public Accessories()
		{
		}

		public List<BookingAccessories> BookingAccessories { get; set; }
    }
}