using PizzaStore.API.Domain.Models;

namespace PizzaStore.API.Domain.Abstractions;

public interface IUnitOfWork
{
    Task Commit();
	void RollBack();
	IRepository<TEntity> GetRepository<TEntity, TRepository>()
        where TEntity : EntityBase;
}
