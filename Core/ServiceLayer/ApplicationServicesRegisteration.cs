using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceApstractionLayer;

namespace ServiceLayer
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services, Microsoft.Extensions.Configuration.ConfigurationManager configuration) 
        {
           Services.AddAutoMapper((x) => { }, typeof(ServiceLayerAssemblyRefrence).Assembly);
           Services.AddScoped<IServiceManager, ServiceManeger>();


            return Services;
        }
       
    }
}
