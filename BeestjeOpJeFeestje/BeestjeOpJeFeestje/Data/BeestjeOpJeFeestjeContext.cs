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

        public DbSet<BeestjeOpJeFeestje.Models.Animal> Animal { get; set; }
    }
}
