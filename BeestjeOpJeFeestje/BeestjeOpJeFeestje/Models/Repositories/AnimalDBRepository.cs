using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Data;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Models.Repositories
{
    public class AnimalDbRepository : IRepository<Animal>
    {
        private readonly BeestjeOpJeFeestjeContext _context;

        public AnimalDbRepository(BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public async Task<Animal> Get(int? id)
        {
            return await _context.Animal.FindAsync(id);
        }

        public async Task<List<Animal>> GetAll()
        {
            return await _context.Animal.ToListAsync();
        }

        public async Task Create(Animal type)
        {
            _context.Animal.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Animal type)
        {
            _context.Update(type);
            _context.Entry(type).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Animal type)
        {
            _context.Animal.Remove(type);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int? id)
        {
            return _context.Animal.Any(e => e.ID == id);
        }

        public Task<bool> Exists(Animal type)
        {
	        throw new NotImplementedException();
        }
    }
}