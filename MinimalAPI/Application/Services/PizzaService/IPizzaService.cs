using MinimalAPI.Application.DTOs;

namespace MinimalAPI.Application.Services.PizzaService
{
    public interface IPizzaService : IDisposable
    {
        Task<IEnumerable<PizzaDto>> GetAllPizzas();
        Task<PizzaDto> GetPizzaById(int id);
        Task Insert(PizzaDto pizzaDto);
        Task Update(PizzaDto pizzaDto);
        Task Delete(PizzaDto pizzaDto);
    }
}
