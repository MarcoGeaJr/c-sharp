using Microsoft.OpenApi.Models;
using PizzaStore.API.Application.Services.PizzaService;

namespace PizzaStore.API.API.Installers
{
    public class ServicesInstaller : IServiceCollectionInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PizzaStore API",
                    Description = "Making the Pizzas you love",
                    Version = "v1"
                });
            });

            services.AddScoped<IPizzaService, PizzaService>();
        }
    }
}
