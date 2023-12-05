using PizzaStore.API.Domain;

namespace PizzaStore.API.Application.Services.PizzaService
{
    public class PizzaService(IPizzaRepository _pizzaRepository)
        : IPizzaService
    {
        private bool disposedValue;

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

        public Task Insert(PizzaDto pizzaDto)
        {
            throw new NotImplementedException();
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
