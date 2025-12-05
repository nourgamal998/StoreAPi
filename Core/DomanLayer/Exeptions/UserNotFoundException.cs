using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Exeptions
{
    public sealed class UserNotFoundException(string Email) 
        : NotFoundExeption ($"User with Email :{Email} was not found.")
    {
    }
}
