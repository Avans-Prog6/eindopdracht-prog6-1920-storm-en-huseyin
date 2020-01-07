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

        public Animal Get()
        {
            return _context.Animal.FirstOrDefault();
        }

        public List<Animal> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Create(Animal type)
        {
            Debug.WriteLine("Hij is hier!");
            //_context.Animal.Add(type);
        }

        public void Update(Animal type)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Animal type)
        {
            throw new System.NotImplementedException();
        }
    }
}