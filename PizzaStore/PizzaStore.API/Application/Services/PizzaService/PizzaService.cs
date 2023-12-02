using PizzaStore.API.Application.DTOs;

namespace PizzaStore.API.Application.Services.PizzaService
{
    public class PizzaService : IPizzaService
    {
        private bool disposedValue;

        public Task<IEnumerable<PizzaDto>> GetAllPizzas()
        {
            throw new NotImplementedException();
        }

        public Task<PizzaDto> GetPizzaById(int id)
        {
            throw new NotImplementedException();
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
                    // TODO: dispose managed state (managed objects)
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
