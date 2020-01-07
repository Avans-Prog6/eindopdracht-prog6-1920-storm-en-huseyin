using System;
using System.Collections.Generic;
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
        public string Name { get;  set; }
        public string Type { get;  set; }
        public double Price { get;  set; }
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
    }
}
