using Microsoft.EntityFrameworkCore;
using PizzaStore.API.Domain;

namespace PizzaStore.API.DataAccess
{
    public class PizzaStoreDbContext(DbContextOptions<PizzaStoreDbContext> dbContextOptions)
    : DbContext(dbContextOptions)
    {
        public const string DefaultSchema = "dbo";
        public const string ConnectionStringName = "PizzaDbConnection";

        public DbSet<Pizza> Pizzas { get; set; } = null!;
    }
}
