using Microsoft.EntityFrameworkCore;
using PizzaStore.API.DataAccess.Repositories;
using PizzaStore.API.Domain.Abstractions;
using PizzaStore.API.Domain.Models;
using PizzaStore.API.Domain.Models.Pizza;

namespace PizzaStore.API.DataAccess.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _context;
		private Dictionary<Type, object> _repositories;

		public UnitOfWork(
			DbContext context)
		{
			_context = context;
			_repositories = new();
		}

		public async Task Commit()
		{
			await _context.SaveChangesAsync();
		}

		public void RollBack()
		{
            foreach (var entry in _context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged))
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

		// TODO
		public TRepository GetRepository<TEntity, TRepository>()
			where TEntity : EntityBase
			where TRepository : IRepository<TEntity>
		{
			if (_repositories.TryGetValue(typeof(TEntity), out var repository))
			{
				return (TRepository)repository;
			}

			throw new InvalidOperationException("The repository has not been registered.");
		}
	}
}
