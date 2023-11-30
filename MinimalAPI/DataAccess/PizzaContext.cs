using Microsoft.EntityFrameworkCore;
using MinimalAPI.Domain;

namespace MinimalAPI.DataAccess
{
    public class PizzaStoreDbContext(DbContextOptions<PizzaStoreDbContext> dbContextOptions)
    : DbContext(dbContextOptions)
    {
        public const string DefaultSchema = "dbo";
        public const string ConnectionStringName = "PizzaDbContext";

        public DbSet<Pizza> Pizzas { get; set; } = null!;
    }
}
