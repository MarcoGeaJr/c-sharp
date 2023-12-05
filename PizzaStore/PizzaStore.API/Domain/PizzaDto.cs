using System.Linq.Expressions;

namespace PizzaStore.API.Domain
{
    public class PizzaDto(int Id, string Name, decimal UnitPrice, bool IsGlutenFree)
    {
        public static Expression<Func<Pizza, PizzaDto>> Selector
            => p => new PizzaDto(p.Id, p.Name!, p.Price, p.IsGlutenFree);
	}
}
