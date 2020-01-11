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
            // modelBuilder.Entity<AnimalAccessories>().HasKey(t => new {t.AnimalId, t.AccessoriesId});
            //
            // modelBuilder.Entity<AnimalAccessories>()
	           //  .HasOne(pt => pt.Animal)
	           //  .WithMany(p => p.AnimalAccessories)
	           //  .HasForeignKey(pt => pt.AnimalId);
            //
            // modelBuilder.Entity<AnimalAccessories>()
	           //  .HasOne(pt => pt.Accessories)
	           //  .WithMany(t => t.AnimalAccessories)
	           //  .HasForeignKey(pt => pt.AccessoriesId);

	           // modelBuilder.Entity<AnimalAccessories>().HasKey();

            modelBuilder.Entity<BookingAnimal>()
	            .HasOne(a => a.Animal)
	            .WithMany(t => t.BookingAnimal)
	            .HasForeignKey(a => a.AnimalId);

            modelBuilder.Entity<BookingAnimal>()
	            .HasOne(a => a.Booking)
	            .WithMany(b => b.BookingAnimals)
	            .HasForeignKey(a => a.BookingId);


            #region AnimalSeed

            modelBuilder.Entity<Animal>().HasData(
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
                    PicturePath = "images/animals/doggo.png"
                },
                new Animal()
                {
                    ID = 6,
                    Name = "Ezel",
                    Type = AnimalTypes.Boerderij,
                    Price = 30.50,
                    PicturePath = "images/animals/donkey.png"
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
                    PicturePath = "images/animals/duck.png"
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
                    ID = 14,
                    Name = "Slang",
                    Type = AnimalTypes.Woestijn,
                    Price = 11.20,
                    PicturePath = "images/animals/slang.png"
                }
            );

            #endregion

            #region accessoriesSeed

            modelBuilder.Entity<Accessories>().HasData(
	            new Accessories
	            {
					ID = 1,
		            Name = "Strikje",
		            PicturePath = "/images/accessories/Picture 1.png",
		            Price = 15.0,
                    AnimalId = 1
                },
	            new Accessories
	            {
		            ID = 2,
		            Name = "Strikje Rood",
		            PicturePath = "/images/accessories/Picture 2.png",
		            Price = 15.0,
                    AnimalId = 1
                },
	            new Accessories
	            {
		            ID = 3,
		            Name = "Hoge Hoed",
		            PicturePath = "/images/accessories/Picture 3.png",
		            Price = 30.0,
                    AnimalId = 2
                },
	            new Accessories
	            {
		            ID = 4,
		            Name = "Kerstmuts",
		            PicturePath = "/images/accessories/Picture 4.png",
		            Price = 25.0,
                    AnimalId = 3
                },
	            new Accessories
	            {
		            ID = 5,
		            Name = "Maracas",
		            PicturePath = "/images/accessories/Picture 5.png",
		            Price = 10.0,
                    AnimalId = 4
                },
	            new Accessories
	            {
		            ID = 6,
		            Name = "Hamer",
		            PicturePath = "/images/accessories/Picture 6.png",
		            Price = 3.0,
                    AnimalId = 5
                },
	            new Accessories
	            {
		            ID = 7,
		            Name = "Vleugels",
		            PicturePath = "/images/accessories/Picture 7.png",
		            Price = 40.0,
                    AnimalId = 6
                },
	            new Accessories
	            {
		            ID = 8,
		            Name = "Afro Haar",
		            PicturePath = "/images/accessories/Picture 8.png",
		            Price = 30.0,
                    AnimalId = 7
                },
	            new Accessories
	            {
		            ID = 9,
		            Name = "Wandelstok",
		            PicturePath = "/images/accessories/Picture 9.png",
		            Price = 15.0,
                    AnimalId = 8
                },
	            new Accessories
	            {
		            ID = 10,
		            Name = "Bot",
		            PicturePath = "/images/accessories/Picture 10.png",
		            Price = 1.0,
                    AnimalId = 9
                },
	            new Accessories
	            {
		            ID = 11,
		            Name = "Hengels",
		            PicturePath = "/images/accessories/Picture 11.png",
		            Price = 25.0,
                    AnimalId = 9
                });

            #endregion

            #region Booking

            DateTime tommorow = DateTime.Today;
            tommorow = tommorow.AddDays(1);
            modelBuilder.Entity<Booking>().HasData(
	            new Booking()
	            {
		            ID = 1,
					Date = tommorow,
	            }
            );

            modelBuilder.Entity<BookingAnimal>().HasData(
				new BookingAnimal()
				{
					AnimalId = 1,
					BookingId = 1,
				},
				new BookingAnimal()
				{
					AnimalId = 4,
					BookingId = 1,
				},
				new BookingAnimal()
				{
					AnimalId = 9,
					BookingId = 1,
				}
            );

            #endregion
        }

        public DbSet<BeestjeOpJeFeestje.Models.Animal> Animal { get; set; }

        public DbSet<BeestjeOpJeFeestje.Models.Accessories> Accessories { get; set; }

		public DbSet<BeestjeOpJeFeestje.Models.Booking> Booking { get; set; }
    }
}
