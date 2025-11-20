using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Exeptions
{
    public class BasketNotFoundExeption (string id)
               : NotFoundExeption($"Basket with Id : {id} Is Not Found")
    {
    }
}
