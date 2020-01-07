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

            modelBuilder.Entity<AnimalAccessories>().HasKey(t => new {t.AnimalId, t.AccessoriesId});
            
            modelBuilder.Entity<AnimalAccessories>()
	            .HasOne(pt => pt.Animal)
	            .WithMany(p => p.AnimalAccessories)
	            .HasForeignKey(pt => pt.AnimalId);
            
            modelBuilder.Entity<AnimalAccessories>()
	            .HasOne(pt => pt.Accessories)
	            .WithMany(t => t.AnimalAccessories)
	            .HasForeignKey(pt => pt.AccessoriesId);
        }

        public DbSet<BeestjeOpJeFeestje.Models.Animal> Animal { get; set; }

        public DbSet<BeestjeOpJeFeestje.Models.Accessories> Accessorieses { get; set; }
    }
}
