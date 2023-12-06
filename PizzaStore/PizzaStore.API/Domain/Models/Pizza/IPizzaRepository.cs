using System.Linq.Expressions;
using PizzaStore.API.Domain.Abstractions;

namespace PizzaStore.API.Domain.Models.Pizza
{
    public interface IPizzaRepository : IRepository<Pizza>
    {
        Task<List<TResult>> GetAllGlutenFree<TResult>(Expression<Func<Pizza, TResult>> selector);
    }
}
