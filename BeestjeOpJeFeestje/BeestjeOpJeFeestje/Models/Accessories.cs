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
		
		public int? AnimalId { get; set; }
		public Animal Animal { get; set; }

		public Accessories(string name, double price, string picturePath = "")
		{
			Name = name;
			Price = price;
			PicturePath = picturePath;
		}

		public Accessories()
		{
		}



	}
}