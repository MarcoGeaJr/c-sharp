using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinimalAPI.API.Requests;
using MinimalAPI.DataAccess;
using MinimalAPI.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PizzaStore API",
        Description = "Making the Pizzas you love",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
    });
}

app.UseHttpsRedirection();

app.MapGet("/pizzas", async (PizzaDb db) => await db.Pizzas.ToListAsync())
    .WithName("GetAllPizzas")
    .WithOpenApi();

app.MapGet("/pizzas/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync(id))
    .WithName("GetPizzaById")
    .WithOpenApi();

app.MapPost("/pizzas", async (PizzaDb db, PostPutPizzaRequest request) =>
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

app.MapPut("/pizzas/{id}", async (PizzaDb db, PostPutPizzaRequest request, int id) =>
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

app.MapDelete("/pizzas/{id}", async (PizzaDb db, int id) =>
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

app.Run();
