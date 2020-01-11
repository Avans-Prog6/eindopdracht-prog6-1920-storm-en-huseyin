using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BeestjeOpJeFeestje.Models
{
    public class Animal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get;  set; }
        [Required]
        public string Type { get;  set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:n} €")]
        public double Price { get;  set; }
        
        [Required]
        [Display(Name="Picture")]
        public string PicturePath { get; set; }
        

        public List<Accessories> Accessories { get; set; }

        public Animal()
        {

        }

        public Animal(string name, double price, string picturePath = "", string type = AnimalTypes.Boerderij)
        {
            Type = type;
            Name = name;
            Price = price;
            PicturePath = picturePath;
        }

        public List<BookingAnimal> BookingAnimal { get; set; }

    }
}
