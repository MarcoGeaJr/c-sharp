namespace PizzaStore.API.Domain.Models.Pizza
{
    public class Pizza : EntityBase
    {
        public Pizza()
        {

        }

        public Pizza(
            string? name,
            string? description,
            decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public Pizza(
            int id,
            string? name,
            string? description,
            decimal price,
            bool isGlutenFree)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            IsGlutenFree = isGlutenFree;
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsGlutenFree { get; set; } = false;
    }
}
