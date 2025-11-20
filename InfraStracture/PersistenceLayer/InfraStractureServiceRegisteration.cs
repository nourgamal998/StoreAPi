using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersistenceLayer.Data.PersistenceLayer.Data;
using PersistenceLayer.Repositoreis;
using StackExchange.Redis;

namespace PersistenceLayer
{
    public static class InfraStractureServiceRegisteration
    {
        public static IServiceCollection AddInfraStractureServices(this IServiceCollection                    Services,IConfiguration _configuration)   
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            });

            Services.AddScoped<IDataSeed, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();

            Services.AddScoped<IBasketReposatory, BasketRepository>();
            Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(_configuration.GetConnectionString("ReddiesConnectionString"));
            });

            return Services;
        }
    }
}
