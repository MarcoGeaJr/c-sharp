using MinimalAPI;
using MinimalAPI.API.Extensions;

WebApplication
    .CreateBuilder(args)
    .BuildIt<IPizzaStoreAssemblyMarker>()
    .MapEndpoints()
    .Run();
