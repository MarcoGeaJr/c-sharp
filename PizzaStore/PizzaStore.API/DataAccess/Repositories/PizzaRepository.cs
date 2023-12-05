using Microsoft.EntityFrameworkCore;
using PizzaStore.API.Domain;
using System.Linq.Expressions;

namespace PizzaStore.API.DataAccess.Repositories
{
	public class PizzaRepository(PizzaStoreDbContext _context)
		: IPizzaRepository, IDisposable
	{
		private bool disposedValue;

		public async Task<List<TResult>> GetAll<TResult>(Expression<Func<Pizza, TResult>> selector)
		{
			return await _context.Pizzas
				.Select(selector)
				.ToListAsync();
		}

		public async Task<List<TResult>> GetAllGlutenFree<TResult>(Expression<Func<Pizza, TResult>> selector)
		{
			return await _context.Pizzas
				.Where(p => p.IsGlutenFree)
				.Select(selector)
				.ToListAsync();
		}

		public async Task<TResult?> GetById<TResult>(int id, Expression<Func<Pizza, TResult>> selector)
		{
			return await _context.Pizzas
				.Where(p => p.Id == id)
				.Select(selector)
				.SingleOrDefaultAsync();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_context.Dispose();
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
