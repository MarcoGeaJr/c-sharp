using Microsoft.EntityFrameworkCore;
using PizzaStore.API.Domain.Abstractions;
using PizzaStore.API.Domain.Models;
using System.Linq.Expressions;

namespace PizzaStore.API.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
	{
		private bool disposedValue;
		private readonly DbContext _context;
		protected readonly DbSet<TEntity> _dbSet;

		public Repository(DbContext context)
		{
			_context = context;
			_dbSet = _context.Set<TEntity>();
		}

		public async Task<List<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
		{
			return await _dbSet
				.Select(selector)
				.ToListAsync();
		}

		public async Task<TResult?> GetById<TResult>(int id, Expression<Func<TEntity, TResult>> selector)
		{
			return await _dbSet
				.Where(e => e.Id == id)
				.Select(selector)
				.SingleOrDefaultAsync();
		}

		public async Task Add(TEntity entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public void Update(TEntity entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(TEntity entity)
		{
			_dbSet.Remove(entity);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_context?.Dispose();
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
