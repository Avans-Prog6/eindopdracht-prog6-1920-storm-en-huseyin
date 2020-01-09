using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeestjeOpJeFeestje.Models;

namespace BeestjeOpJeFeestje.Data
{
    public class BeestjeOpJeFeestjeContext : DbContext
    {
        public BeestjeOpJeFeestjeContext (DbContextOptions<BeestjeOpJeFeestjeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
	        base.OnModelCreating(modelBuilder);

	        modelBuilder.Entity<BookingAnimal>().HasKey(t => new {t.AnimalId, t.BookingId});
            modelBuilder.Entity<AnimalAccessories>().HasKey(t => new {t.AnimalId, t.AccessoriesId});
            
            modelBuilder.Entity<AnimalAccessories>()
	            .HasOne(pt => pt.Animal)
	            .WithMany(p => p.AnimalAccessories)
	            .HasForeignKey(pt => pt.AnimalId);
            
            modelBuilder.Entity<AnimalAccessories>()
	            .HasOne(pt => pt.Accessories)
	            .WithMany(t => t.AnimalAccessories)
	            .HasForeignKey(pt => pt.AccessoriesId);

            modelBuilder.Entity<BookingAnimal>()
	            .HasOne(a => a.Animal)
	            .WithMany(t => t.BookingAnimal)
	            .HasForeignKey(a => a.AnimalId);

            modelBuilder.Entity<BookingAnimal>()
	            .HasOne(a => a.Booking)
	            .WithMany(b => b.BookingAnimals)
	            .HasForeignKey(a => a.BookingId);


            #region accessoriesSeed

            modelBuilder.Entity<Accessories>().HasData(
	            new Accessories
	            {
					ID = 1,
		            Name = "Strikje",
		            PicturePath = "/images/accessories/Picture 1.png",
		            Price = 15.0,
	            },
	            new Accessories
	            {
		            ID = 2,
		            Name = "Strikje Rood",
		            PicturePath = "/images/accessories/Picture 2.png",
		            Price = 15.0,
	            },
	            new Accessories
	            {
		            ID = 3,
		            Name = "Hoge Hoed",
		            PicturePath = "/images/accessories/Picture 3.png",
		            Price = 30.0,
	            },
	            new Accessories
	            {
		            ID = 4,
		            Name = "Kerstmuts",
		            PicturePath = "/images/accessories/Picture 4.png",
		            Price = 25.0,
	            },
	            new Accessories
	            {
		            ID = 5,
		            Name = "Maracas",
		            PicturePath = "/images/accessories/Picture 5.png",
		            Price = 10.0,
	            },
	            new Accessories
	            {
		            ID = 6,
		            Name = "Hamer",
		            PicturePath = "/images/accessories/Picture 6.png",
		            Price = 3.0,
	            },
	            new Accessories
	            {
		            ID = 7,
		            Name = "Vleugels",
		            PicturePath = "/images/accessories/Picture 7.png",
		            Price = 40.0,
	            },
	            new Accessories
	            {
		            ID = 8,
		            Name = "Afro Haar",
		            PicturePath = "/images/accessories/Picture 8.png",
		            Price = 30.0,
	            },
	            new Accessories
	            {
		            ID = 9,
		            Name = "Wandelstok",
		            PicturePath = "/images/accessories/Picture 9.png",
		            Price = 15.0,
	            },
	            new Accessories
	            {
		            ID = 10,
		            Name = "Bot",
		            PicturePath = "/images/accessories/Picture 10.png",
		            Price = 1.0,
	            },
	            new Accessories
	            {
		            ID = 11,
		            Name = "Hengels",
		            PicturePath = "/images/accessories/Picture 11.png",
		            Price = 25.0,
	            });

            #endregion
        }

        public DbSet<BeestjeOpJeFeestje.Models.Animal> Animal { get; set; }

        public DbSet<BeestjeOpJeFeestje.Models.Accessories> Accessories { get; set; }

		public DbSet<BeestjeOpJeFeestje.Models.Booking> Booking { get; set; }
    }
}
