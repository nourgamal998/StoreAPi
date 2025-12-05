using DomanLayer.Contracts;
using DomanLayer.Models.Identity_models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersistenceLayer.Data.PersistenceLayer.Data;
using PersistenceLayer.Identity;
using PersistenceLayer.Repositoreis;
using StackExchange.Redis;

namespace PersistenceLayer
{
    public static class InfraStractureServiceRegisteration
    {
        private const string RedisConfigKey = "RedisConnectionString";

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
                return ConnectionMultiplexer.Connect(_configuration.GetConnectionString(RedisConfigKey));
            });

            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("IdentityConnection"));
            });

            Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<StoreIdentityDbContext>();
                
            return Services;

        }
    }
}
