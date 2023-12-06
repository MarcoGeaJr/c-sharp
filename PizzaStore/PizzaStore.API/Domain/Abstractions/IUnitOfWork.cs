using PizzaStore.API.Domain.Models;

namespace PizzaStore.API.Domain.Abstractions;

public interface IUnitOfWork
{
	Task CommitAsync();
	void RollBack();
	IRepository<TEntity> GetRepository<TEntity>()
			where TEntity : EntityBase;
}
