using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Data;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Models.Repositories
{
    public class AnimalDBRepository : IRepository<Animal>
    {
        private readonly BeestjeOpJeFeestjeContext _context;

        public AnimalDBRepository(BeestjeOpJeFeestjeContext context)
        {
            _context = context;
        }

        public async Task<Animal> Get(int? ID)
        {
            return await _context.Animal.FindAsync(ID);
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

        public bool Exists(int? ID)
        {
            return _context.Animal.Any(e => e.ID == ID);
        }
    }
}