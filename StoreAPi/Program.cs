
using DomanLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersistenceLayer;
using PersistenceLayer.Data;
using PersistenceLayer.Data.PersistenceLayer.Data;
using PersistenceLayer.Repositoreis;
using ServiceApstractionLayer;
using ServiceLayer;

namespace StoreAPi
{
    public class Program
    {
        public static string? DefaultConnection { get; private set; }

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at
            // https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options => 
            {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));   
            });

             builder.Services.AddScoped<IDataSeed, DataSeeding>();
             builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
             builder.Services.AddAutoMapper((x)=> { },typeof(ServiceLayerAssemblyRefrence).Assembly);
             builder.Services.AddScoped<IServiceManager, ServiceManeger>();

            var app = builder.Build();

            var scop= app.Services.CreateScope();
            var SeedOpj = scop.ServiceProvider.GetRequiredService<IDataSeed>();
            await SeedOpj.DataSeedAsync();



            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
