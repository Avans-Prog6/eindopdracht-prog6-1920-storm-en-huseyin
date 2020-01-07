using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public Animal Get(int ID)
        {
            return _context.Animal.FirstOrDefault(s => s.ID == ID);
        }

        public List<Animal> GetAll()
        {
            return _context.Animal.ToList();
        }

        public void Create(Animal type)
        {
            _context.Animal.Add(type);
            _context.SaveChanges();
            _context.Dispose();
        }

        public void Update(Animal type)
        {
            _context.Entry(type).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Animal type)
        {
            _context.Animal.Attach(type);
            _context.Animal.Remove(type);
            _context.SaveChanges();
        }
    }
}