using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
        [StringLength(30, MinimumLength = 2)]
        public string Type { get;  set; }
        [Required]
        public double Price { get;  set; }
        [Display(Name="Picture"), Column("Picture")]
        public string PicturePath { get; set; }

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

        public List<AnimalAccessories> AnimalAccessories { get; set; }

        public List<BookingAnimal> BookingAnimal { get; set; }
    }
}
