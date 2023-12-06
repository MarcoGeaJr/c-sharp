using PizzaStore.API.Domain.Abstractions;
using PizzaStore.API.Domain.Models.Pizza;

namespace PizzaStore.API.Application.Services.PizzaService
{
	public class PizzaService : IPizzaService
	{
		private bool disposedValue;

		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<Pizza> _pizzaRepository;

		public PizzaService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_pizzaRepository = _unitOfWork.GetRepository<Pizza>();
		}

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

		public async Task<int> Insert(PizzaDto pizzaDto)
		{
			var pizza = new Pizza(pizzaDto.Name, pizzaDto.Description, pizzaDto.UnitPrice);

			pizza.IsGlutenFree = pizzaDto.IsGlutenFree;

			await _pizzaRepository.AddAsync(pizza);

			await _unitOfWork.CommitAsync();

			return pizza.Id;
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
