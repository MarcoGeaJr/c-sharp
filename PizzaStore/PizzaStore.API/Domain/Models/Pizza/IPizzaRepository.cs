using PizzaStore.API.Domain.Abstractions;
using System.Linq.Expressions;

namespace PizzaStore.API.Domain.Models.Pizza
{
	// TODO
	public interface IPizzaRepository : IRepository<Pizza>
	{
		Task<List<TResult>> GetAllGlutenFree<TResult>(Expression<Func<Pizza, TResult>> selector);
	}
}
