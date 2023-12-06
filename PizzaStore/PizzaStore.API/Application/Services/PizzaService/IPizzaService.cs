using PizzaStore.API.Domain.Models.Pizza;

namespace PizzaStore.API.Application.Services.PizzaService
{
	public interface IPizzaService : IDisposable
	{
		Task<IEnumerable<PizzaDto>> GetAllPizzas();
		Task<PizzaDto?> GetPizzaById(int id);
		Task<int> Insert(PizzaDto pizzaDto);
		Task Update(PizzaDto pizzaDto);
		Task Delete(PizzaDto pizzaDto);
	}
}
