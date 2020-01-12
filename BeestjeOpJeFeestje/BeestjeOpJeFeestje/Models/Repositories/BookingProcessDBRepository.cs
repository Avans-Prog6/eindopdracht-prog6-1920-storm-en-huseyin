using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Data;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Models.Repositories
{
	public class BookingProcessDBRepository : IRepository<BookingProcess>
	{
		private readonly BeestjeOpJeFeestjeContext _context;

		public BookingProcessDBRepository(BeestjeOpJeFeestjeContext context)
		{
			_context = context;
		}

		public async Task<BookingProcess> Get(int? ID)
		{
			return await _context.BookingProcesses.Include(e => e.BookingProcessAccessories)
				.Include(e => e.BookingProcessAnimals)
				.Include(e => e.ClientInfo)
				.SingleOrDefaultAsync(d => d.ID == ID);
		}

		public Task<List<BookingProcess>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<List<BookingProcess>> Find(params int[] keyValues)
		{
			throw new NotImplementedException();
		}

		public async Task Create(BookingProcess type)
		{
			type.ClientInfo = null;
			_context.BookingProcesses.Add(type);
			await _context.SaveChangesAsync();
		}

		public Task Update(BookingProcess type)
		{
			throw new NotImplementedException();
		}

		public Task Delete(BookingProcess type)
		{
			throw new NotImplementedException();
		}

		public bool Exists(int? ID)
		{
			throw new NotImplementedException();
		}

		public bool Exists(BookingProcess type)
		{
			throw new NotImplementedException();
		}
	}
}