using System.Linq.Expressions;

namespace PizzaStore.API.Domain.Models.Pizza;

public class PizzaDto(int Id, string Name, string Description, decimal UnitPrice, bool IsGlutenFree)
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsGlutenFree { get; set; }

    public static Expression<Func<Pizza, PizzaDto>> Selector
        => p => new PizzaDto(p.Id, p.Name!, p.Description!, p.Price, p.IsGlutenFree);
}
