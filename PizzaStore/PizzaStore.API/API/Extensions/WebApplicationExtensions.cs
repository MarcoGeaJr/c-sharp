using Microsoft.EntityFrameworkCore;
using PizzaStore.API.API.Requests;
using PizzaStore.API.DataAccess;
using PizzaStore.API.Domain;

namespace PizzaStore.API.API.Extensions
{
	public static class WebApplicationExtensions
	{
		public static WebApplication MapEndpoints(this WebApplication app)
		{
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
				});
			}

			app.UseHttpsRedirection();


			app.MapGet("/pizzas", async (PizzaStoreDbContext db) =>
			{
				var pizzas = await db.Pizzas.ToListAsync();

				return Results.Ok(pizzas);
			})
				.WithName("GetAllPizzas")
				.WithOpenApi();

			app.MapGet("/pizzas/{id}", async (PizzaStoreDbContext db, int id) => await db.Pizzas.FindAsync(id))
				.WithName("GetPizzaById")
			.WithOpenApi();

			app.MapPost("/pizzas", async (PizzaStoreDbContext db, PostPizzaRequest request) =>
			{
				if (!request.IsValid())
				{
					return Results.BadRequest("Os dados da pizza não estão válidos.");
				}

				var pizza = new Pizza(request.Name, request.Description, request.Price);

				pizza.IsGlutenFree = request.IsGlutenFree;

				await db.Pizzas.AddAsync(pizza);
				await db.SaveChangesAsync();

				return Results.Created($"/pizzas/{pizza.Id}", pizza.Id);
			})
				.WithName("PostNewPizza")
				.WithOpenApi();

			app.MapPut("/pizzas/{id}", async (PizzaStoreDbContext db, PutPizzaRequest request, int id) =>
			{
				if (id != request.Id || !request.IsValid())
				{
					return Results.BadRequest("Os dados da pizza não estão válidos.");
				}

				var pizza = await db.Pizzas.FindAsync(id);

				if (pizza is null)
				{
					return Results.NotFound();
				}

				pizza.Name = request.Name;
				pizza.Description = request.Description;
				pizza.Price = request.Price;
				pizza.IsGlutenFree = request.IsGlutenFree;

				await db.SaveChangesAsync();

				return Results.NoContent();
			})
				.WithName("UpdatePizza")
				.WithOpenApi();

			app.MapDelete("/pizzas/{id}", async (PizzaStoreDbContext db, int id) =>
			{
				var pizza = await db.Pizzas.FindAsync(id);

				if (pizza is null)
				{
					return Results.NotFound();
				}

				db.Pizzas.Remove(pizza);

				await db.SaveChangesAsync();

				return Results.Ok();
			})
				.WithName("RemovePizza")
				.WithOpenApi();

			return app;
		}
	}
}
