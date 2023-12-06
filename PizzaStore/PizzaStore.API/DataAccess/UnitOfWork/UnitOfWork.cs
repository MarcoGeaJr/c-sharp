using Microsoft.EntityFrameworkCore;
using PizzaStore.API.DataAccess.Repositories;
using PizzaStore.API.Domain.Abstractions;
using PizzaStore.API.Domain.Models;

namespace PizzaStore.API.DataAccess.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly PizzaStoreDbContext _context;
		private readonly Dictionary<Type, object> _repositories;

		public UnitOfWork(
			PizzaStoreDbContext context)
		{
			_context = context;
			_repositories = new();
		}

		public async Task CommitAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void RollBack()
		{
			var modifiedEntries = _context.ChangeTracker
				.Entries()
				.Where(x => x.State != EntityState.Unchanged)
				.ToList();

			foreach (var entry in modifiedEntries)
			{
				switch (entry.State)
				{
					case EntityState.Modified:
						entry.CurrentValues.SetValues(entry.OriginalValues);
						entry.State = EntityState.Unchanged;
						break;
					case EntityState.Added:
						entry.State = EntityState.Detached;
						break;
					case EntityState.Deleted:
						entry.State = EntityState.Unchanged;
						break;
				}
			}
		}

		public IRepository<TEntity> GetRepository<TEntity>()
			where TEntity : EntityBase
		{
			if (_repositories.ContainsKey(typeof(TEntity)))
			{
				return (IRepository<TEntity>)_repositories[typeof(TEntity)];
			}

			var repository = new Repository<TEntity>(_context);
			_repositories.Add(typeof(TEntity), repository);
			return repository;
		}
	}
}
