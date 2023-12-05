using System.Linq.Expressions;

namespace PizzaStore.API.Domain
{
	public interface IPizzaRepository : IDisposable
	{
		Task<List<TResult>> GetAll<TResult>(Expression<Func<Pizza, TResult>> selector);
		Task<List<TResult>> GetAllGlutenFree<TResult>(Expression<Func<Pizza, TResult>> selector);
		Task<TResult?> GetById<TResult>(int id, Expression<Func<Pizza, TResult>> selector);
	}
}
