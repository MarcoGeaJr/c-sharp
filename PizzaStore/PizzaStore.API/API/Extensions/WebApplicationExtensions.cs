using Microsoft.EntityFrameworkCore;
using PizzaStore.API.API.Requests;
using PizzaStore.API.Application.Services.PizzaService;
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


			app.MapGet("/pizzas", async (IPizzaService pizzaService) =>
			{
				return Results.Ok(await pizzaService.GetAllPizzas());
			})
				.WithName("GetAllPizzas")
				.WithOpenApi();

			app.MapGet("/pizzas/{id}", async (IPizzaService pizzaService, int id) => await pizzaService.GetPizzaById(id))
				.WithName("GetPizzaById")
			.WithOpenApi();

			app.MapPost("/pizzas", async (IPizzaService pizzaService, PostPizzaRequest request) =>
			{
				if (!request.IsValid())
				{
					return Results.BadRequest("Os dados da pizza não estão válidos.");
				}

				var pizzaDto = new PizzaDto(0, request.Name, request.Description, request.Price, request.IsGlutenFree);

				var pizzaId = await pizzaService.Insert(pizzaDto);

				return Results.Created($"/pizzas/{pizzaId}", pizzaId);
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
