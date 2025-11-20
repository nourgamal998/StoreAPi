using DomanLayer.Contracts;
using StoreAPi.CustomMiddleweres;

namespace StoreAPi.Extentions
{
    public static class WebApplicationRegisteration
    {
        public async static Task SeedDatabaseAsync(this WebApplication app)
        {
            var scop = app.Services.CreateScope();
            var SeedOpj = scop.ServiceProvider.GetRequiredService<IDataSeed>();
            await SeedOpj.DataSeedAsync();
        }

        public static IApplicationBuilder UseCustomExeptionMiddleWare(this IApplicationBuilder app)
        {

            app.UseMiddleware<CustomExeptionHandlerMiddlewere>();
            return app;
        }

    }
}
