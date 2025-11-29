using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Contracts
{
    public interface IDataSeed
    {
        //  Task DataSeedAsync();
       public Task DataSeedAsync();
       public Task IdentityDataSeedAsync();

    }
}
