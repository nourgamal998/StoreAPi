using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using DomanLayer.Models;
using PersistenceLayer.Data.PersistenceLayer.Data;

namespace PersistenceLayer.Repositoreis
{
    public class UnitOfWork (StoreDbContext _DbContext): IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<IEntity, Tkey> GetRepository<IEntity, Tkey>() where IEntity : BaseEntity<Tkey>
        {
            var typeName = typeof(IEntity).Name;

            if (_repositories.ContainsKey(typeName))
                return (IGenericRepository<IEntity, Tkey>)_repositories[typeName];

            else
            { 
              var repo = new GenaricRepository<IEntity, Tkey>(_DbContext);
                _repositories.Add(typeName, repo);
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _DbContext.SaveChangesAsync();
       
    }
}
