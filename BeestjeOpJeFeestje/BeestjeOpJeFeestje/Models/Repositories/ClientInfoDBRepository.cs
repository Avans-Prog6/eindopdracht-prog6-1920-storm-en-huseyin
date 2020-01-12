using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeestjeOpJeFeestje.Data;
using Microsoft.EntityFrameworkCore;

namespace BeestjeOpJeFeestje.Models.Repositories
{
	public class ClientInfoDBRepository : IRepository<ClientInfo>
	{
		private readonly BeestjeOpJeFeestjeContext _context;
		public ClientInfoDBRepository(BeestjeOpJeFeestjeContext context)
		{
			_context = context;
		}


		public Task<ClientInfo> Get(int? ID)
		{
			throw new NotImplementedException();
		}

		public Task<List<ClientInfo>> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<List<ClientInfo>> Find(params int[] keyValues)
		{
			throw new NotImplementedException();
		}

		public async Task<ClientInfo> Find(string email)
		{
			return await _context.ClientInfo.Where(e => e.Email == email).FirstOrDefaultAsync();
		}

		public async Task Create(ClientInfo type)
		{
			_context.ClientInfo.Add(type);
			await _context.SaveChangesAsync();
		}

		public Task Update(ClientInfo type)
		{
			throw new NotImplementedException();
		}

		public Task Delete(ClientInfo type)
		{
			throw new NotImplementedException();
		}

		public bool Exists(int? ID)
		{
			throw new NotImplementedException();
		}

		public bool Exists(ClientInfo type)
		{
			throw new NotImplementedException();
		}
	}
}
