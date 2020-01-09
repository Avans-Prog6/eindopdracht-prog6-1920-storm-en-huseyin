using System;
using System.Linq;
using BeestjeOpJeFeestje.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BeestjeOpJeFeestje.Models
{
	public static class SeedAccessories
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

				context.Accessories.AddRange(
					new Accessories
					{
						Name = "Strikje",
						PicturePath = "/images/accessories/Picture 1.png",
						Price = 15.0,
					},
					new Accessories
					{
						Name = "Strikje Rood",
						PicturePath = "/images/accessories/Picture 2.png",
						Price = 15.0,
					},
					new Accessories
					{
						Name = "Hoge Hoed",
						PicturePath = "/images/accessories/Picture 3.png",
						Price = 30.0,
					},
					new Accessories
					{
						Name = "Kerstmuts",
						PicturePath = "/images/accessories/Picture 4.png",
						Price = 25.0,
					},
					new Accessories
					{
						Name = "Maracas",
						PicturePath = "/images/accessories/Picture 5.png",
						Price = 10.0,
					},
					new Accessories
					{
						Name = "Hamer",
						PicturePath = "/images/accessories/Picture 6.png",
						Price = 3.0,
					},
					new Accessories
					{
						Name = "Vleugels",
						PicturePath = "/images/accessories/Picture 7.png",
						Price = 40.0,
					},
					new Accessories
					{
						Name = "Afro Haar",
						PicturePath = "/images/accessories/Picture 8.png",
						Price = 30.0,
					},
					new Accessories
					{
						Name = "Wandelstok",
						PicturePath = "/images/accessories/Picture 9.png",
						Price = 15.0,
					},
					new Accessories
					{
						Name = "Bot",
						PicturePath = "/images/accessories/Picture 10.png",
						Price = 1.0,
					},
					new Accessories
					{
						Name = "Hengels",
						PicturePath = "/images/accessories/Picture 11.png",
						Price = 25.0,
					}
				);

				context.SaveChanges();
			}
		}
	}
}
