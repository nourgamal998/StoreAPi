using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApstractionLayer
{
    public interface IServiceManager
    {
 
            public IProductService ProductService { get; }
            public IBasketService BasketService { get; }
        }
    }

