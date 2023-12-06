using PizzaStore.API.Domain.Models;
using System.Linq.Expressions;

namespace PizzaStore.API.Domain.Abstractions
{
	public interface IRepository<TEntity> : IDisposable where TEntity : EntityBase
	{
		Task<List<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector);
		Task<TResult?> GetById<TResult>(int id, Expression<Func<TEntity, TResult>> selector);

		Task AddAsync(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
	}
}
