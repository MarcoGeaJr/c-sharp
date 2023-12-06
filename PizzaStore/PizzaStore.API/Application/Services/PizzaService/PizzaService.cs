using PizzaStore.API.Domain.Models.Pizza;

namespace PizzaStore.API.Application.Services.PizzaService
{
    public class PizzaService : IPizzaService
    {
        private bool disposedValue;

        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
            => (_pizzaRepository) = (pizzaRepository);

        public async Task<IEnumerable<PizzaDto>> GetAllPizzas()
        {
            return await _pizzaRepository
                .GetAll(PizzaDto.Selector);
		}

        public async Task<PizzaDto?> GetPizzaById(int id)
        {
			return await _pizzaRepository
				.GetById(id, PizzaDto.Selector);
		}

        public Task<int> Insert(PizzaDto pizzaDto)
        {
			var pizza = new Pizza(pizzaDto.Name, pizzaDto.Description, pizzaDto.UnitPrice);

			pizza.IsGlutenFree = pizzaDto.IsGlutenFree;

			_pizzaRepository.Add(pizza);

			await _pizzaRepository.SaveChangesAsync();
		}

        public Task Update(PizzaDto pizzaDto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(PizzaDto pizzaDto)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _pizzaRepository?.Dispose();

				}

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
