using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Exeptions
{
    public class ProductNotFoundException(int id ) : NotFoundException($"Product with Id : {id} Is Not Found")
    {
    }
}
