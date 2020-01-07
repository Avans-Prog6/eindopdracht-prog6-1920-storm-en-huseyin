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
        public string Name { get; private set; }
        public string Type { get; private set; }
        public double Price { get; private set; }
        public string PicturePath { get; private set; }

        public Animal(string name, double price, string picturePath = "", string type = AnimalTypes.Boerderij)
        {
            Type = type;
            Name = name;
            Price = price;
            PicturePath = picturePath;
        }
    }
}
