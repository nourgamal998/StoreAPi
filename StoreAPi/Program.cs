using DomanLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens.Experimental;
using PersistenceLayer;
using PersistenceLayer.Data;
using PersistenceLayer.Data.PersistenceLayer.Data;
using PersistenceLayer.Repositoreis;
using ServiceApstractionLayer;
using ServiceLayer;
using StoreAPi.CustomMiddleweres;
using Shared.ErrorModels;
using StoreAPi.Factories;

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
            #region Register User-Defined Services
            builder.Services.AddScoped<IDataSeed, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            #region Mapping Register 
            builder.Services.AddAutoMapper((x) => { }, typeof(ServiceLayerAssemblyRefrence).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManeger>();



            builder.Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            }); 

            #endregion
            #endregion

            var app = builder.Build();
            

            #region Seed Database
            var scop = app.Services.CreateScope();
            var SeedOpj = scop.ServiceProvider.GetRequiredService<IDataSeed>();
            await SeedOpj.DataSeedAsync();
            #endregion

            #region //Configure the HTTP request pipeline.
            app.UseMiddleware<CustomExeptionHandlerMiddlewere>();
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
            #endregion
        }
    }
}
