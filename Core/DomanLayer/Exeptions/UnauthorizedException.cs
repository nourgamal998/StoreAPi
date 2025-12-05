using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Exeptions
{
    public sealed class UnauthorizedException (string Message="InValid Email Or Password")
        : Exception(Message)
    {
    }
}
