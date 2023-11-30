using Microsoft.EntityFrameworkCore;
using MinimalAPI.API.Requests;
using MinimalAPI.DataAccess;
using MinimalAPI.Domain;

namespace MinimalAPI.API.Extensions
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


            app.MapGet("/pizzas", async (PizzaStoreDbContext db) => await db.Pizzas.ToListAsync())
                .WithName("GetAllPizzas")
                .WithOpenApi();

            app.MapGet("/pizzas/{id}", async (PizzaStoreDbContext db, int id) => await db.Pizzas.FindAsync(id))
                .WithName("GetPizzaById")
                .WithOpenApi();

            app.MapPost("/pizzas", async (PizzaStoreDbContext db, PostPutPizzaRequest request) =>
            {
                if (!request.IsValid())
                {
                    return Results.BadRequest("Os dados da pizza não estão válidos.");
                }

                var pizza = new Pizza(request.Name, request.Description);

                await db.Pizzas.AddAsync(pizza);
                await db.SaveChangesAsync();

                return Results.Created($"/pizzas/{pizza.Id}", pizza.Id);
            })
                .WithName("PostNewPizza")
                .WithOpenApi();

            app.MapPut("/pizzas/{id}", async (PizzaStoreDbContext db, PostPutPizzaRequest request, int id) =>
            {
                if (!request.IsValid())
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
