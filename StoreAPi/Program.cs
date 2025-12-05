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
using StoreAPi.Extentions;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Identity;
using DomanLayer.Models.Identity_models;
using PersistenceLayer.Identity;

namespace StoreAPi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the DI container.

            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();


            #region Register User-Defined Services
            builder.Services.AddWebApplicationServices();
           
            builder.Services.AddInfraStractureServices(builder.Configuration);

            builder.Services.AddApplicationServices(builder.Configuration);


            #endregion
            var app = builder.Build();
            await app.SeedDatabaseAsync();


            #region 
            //Configure the HTTP request pipeline.

            app.UseCustomExeptionMiddleWare();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleware();
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
