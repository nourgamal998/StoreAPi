using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models;

namespace DomanLayer.Contracts
{
    public interface IUnitOfWork 
    {

        //public IGenericRepository<Product, int> ProductRepository{get; }
        public IGenericRepository<IEntity, Tkey> GetRepository <IEntity,Tkey>()
                                                where IEntity : BaseEntity<Tkey>;  
        Task<int> SaveChangesAsync();
    }
}
