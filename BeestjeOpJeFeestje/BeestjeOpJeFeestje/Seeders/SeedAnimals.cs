using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Data;
using BeestjeOpJeFeestje.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BeestjeOpJeFeestje.Seeders
{
    public static class SeedAnimals
    {
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new BeestjeOpJeFeestjeContext(
				serviceProvider.GetRequiredService<
					DbContextOptions<BeestjeOpJeFeestjeContext>>()))
			{
				// Look for any movies.
				if (context.Accessories.Any())
				{
					return;   // DB has been seeded
				}

				context.AddRange(
                    new Animal()
                    {
						ID = 1,
						Name = "Aap",
						Type = AnimalTypes.Jungle,
						Price = 4.50,
                        PicturePath = "images/animals/aap.png"
                    },

                    new Animal()
                    {
                        ID = 2,
                        Name = "Olifant",
                        Type = AnimalTypes.Jungle,
                        Price = 16.50,
                        PicturePath = "images/animals/olifant.png"
                    },

                    new Animal()
                    {
                        ID = 3,
                        Name = "Zebra",
                        Type = AnimalTypes.Jungle,
                        Price = 1.50,
                        PicturePath = "images/animals/zebra.png"
                    },


                new Animal()
                    {
                        ID = 4,
                        Name = "Leeuw",
                        Type = AnimalTypes.Jungle,
                        Price = 29.50,
                        PicturePath = "images/animals/leeuw.png"
                },

                    new Animal()
                    {
                        ID = 5,
                        Name = "Hond",
                        Type = AnimalTypes.Boerderij,
                        Price = 7.50,
                        PicturePath = "images/animals/hond.png"
                    },

                    new Animal()
                    {
                        ID = 6,
                        Name = "Ezel",
                        Type = AnimalTypes.Boerderij,
                        Price = 30.50,
                        PicturePath = "images/animals/ezel.png"
                    },


                    new Animal()
                    {
                        ID = 7,
                        Name = "Koe",
                        Type = AnimalTypes.Boerderij,
                        Price = 1.75,
                        PicturePath = "images/animals/koe.png"
                    },

                    new Animal()
                    {
                        ID = 8,
                        Name = "Eend",
                        Type = AnimalTypes.Boerderij,
                        Price = 0.75,
                        PicturePath = "images/animals/eend.png"
                    },

                    new Animal()
                    {
                        ID = 9,
                        Name = "Kuiken",
                        Type = AnimalTypes.Boerderij,
                        Price = 3.75,
                        PicturePath = "images/animals/kuiken.png"
                    },

                    new Animal()
                    {
                        ID = 10,
                        Name = "Pinguin",
                        Type = AnimalTypes.Sneeuw,
                        Price = 40.00,
                        PicturePath = "images/animals/pingwing.png"
                    },

                    new Animal()
                    {
                        ID = 11,
                        Name = "Ijsbeer",
                        Type = AnimalTypes.Sneeuw,
                        Price = 11.75,
                        PicturePath = "images/animals/ijsbeer.png"
                    },

                    new Animal()
                    {
                        ID = 12,
                        Name = "Zeehond",
                        Type = AnimalTypes.Sneeuw,
                        Price = 23.75,
                        PicturePath = "images/animals/zeehond.png"
                    },

                    new Animal()
                    {
                        ID = 13,
                        Name = "Kameel",
                        Type = AnimalTypes.Woestijn,
                        Price = 55.20,
                        PicturePath = "images/animals/kameel.png"
                    },

                    new Animal()
                    {
                        ID = 13,
                        Name = "Slang",
                        Type = AnimalTypes.Woestijn,
                        Price = 11.20,
                        PicturePath = "images/animals/slang.png"
                    }
                );

				context.SaveChanges();
			}
		}
	}
}
