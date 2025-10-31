
using DomanLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer;
using PersistenceLayer.Data;
using PersistenceLayer.Data.PersistenceLayer.Data;

namespace StoreAPi
{
    public class Program
    {
        public static string DefaultConnection { get; private set; }

        public static void Main(string[] args)
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

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            var app = builder.Build();

            using var scop= app.Services.CreateScope();
            var SeedOpj = scop.ServiceProvider.GetRequiredService<IDataSeeding>();
            SeedOpj.DataSeed();

            //Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
