using Microsoft.AspNetCore.Mvc;
using StoreAPi.Factories;

namespace StoreAPi.Extentions
{
    public static class ServiceRegisteration 
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
           
            return Services;
        }
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
        {
           Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });

            return Services;
        }
    }
}
