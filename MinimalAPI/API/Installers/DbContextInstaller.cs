﻿using Microsoft.EntityFrameworkCore;
using MinimalAPI.DataAccess;

namespace MinimalAPI.API.Installers
{
    public class DbContextInstaller : IServiceCollectionInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var conStr = configuration.GetConnectionString(PizzaStoreDbContext.ConnectionStringName);


            services.AddDbContext<PizzaStoreDbContext>(options =>
            {
                //options.UseSqlServer(conStr);
                options.UseInMemoryDatabase(conStr);
            });

            //services.AddScoped<ITagRepository, TagRepository>();
        }
    }
}
