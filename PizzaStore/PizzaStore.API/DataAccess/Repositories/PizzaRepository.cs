using Microsoft.EntityFrameworkCore;
using PizzaStore.API.Domain.Models.Pizza;
using PizzaStore.API.Domain.Pizza;
using System.Linq.Expressions;

namespace PizzaStore.API.DataAccess.Repositories;

public class PizzaRepository : Repository<Pizza>, IPizzaRepository
{
	public PizzaRepository(DbContext context) : base(context)
	{
	}

	public async Task<List<TResult>> GetAllGlutenFree<TResult>(Expression<Func<Pizza, TResult>> selector)
	{
		return await _dbSet
			.Where(p => p.IsGlutenFree)
			.Select(selector)
			.ToListAsync();
	}
}
