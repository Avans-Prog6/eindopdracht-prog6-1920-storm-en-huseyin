using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Data;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Models.Repositories
{
	public class AccessoriesDBRepository : IRepository<Accessories>
	{
		private readonly BeestjeOpJeFeestjeContext _context;

		public AccessoriesDBRepository(BeestjeOpJeFeestjeContext context)
		{
			_context = context;
		}

		public async Task<Accessories> Get(int? ID)
		{
			return await _context.Accessorieses.FindAsync(ID);
		}

		public async Task<List<Accessories>> GetAll()
		{
			return await _context.Accessorieses.ToListAsync();
		}

		public async Task Create(Accessories type)
		{
			_context.Accessorieses.Add(type);
			await _context.SaveChangesAsync();
		}

		public async Task Update(Accessories type)
		{
			_context.Update(type);
			_context.Entry(type).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Accessories type)
		{
			_context.Accessorieses.Remove(type);
			await _context.SaveChangesAsync();
		}

		public bool Exists(int? ID)
		{
			return _context.Accessorieses.Any(e => e.ID == ID);
		}
	}
}
