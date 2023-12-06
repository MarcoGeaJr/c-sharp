﻿using Microsoft.EntityFrameworkCore;
using PizzaStore.API.DataAccess;
using PizzaStore.API.DataAccess.UnitOfWork;
using PizzaStore.API.Domain.Abstractions;

namespace PizzaStore.API.API.Installers
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

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			//services.AddScoped<IPizzaRepository, PizzaRepository>();
		}
	}
}
