using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain;

namespace MinimalAPI.DataAccess
{
    class PizzaDb : DbContext
    {
        public PizzaDb(DbContextOptions options) : base(options) { }
        public DbSet<Pizza> Pizzas { get; set; } = null!;
    }
}
