using PizzaStore.API;
using PizzaStore.API.API.Extensions;

WebApplication
    .CreateBuilder(args)
    .BuildIt<IPizzaStoreAssemblyMarker>()
    .MapEndpoints()
    .Run();
