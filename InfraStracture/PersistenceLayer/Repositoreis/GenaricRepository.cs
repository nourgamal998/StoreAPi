using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Data.PersistenceLayer.Data;

namespace PersistenceLayer.Repositoreis
{
    public class GenaricRepository<TEntity, Tkey>(StoreDbContext _DbContect) : IGenericRepository<TEntity, Tkey>
        where TEntity : DomanLayer.Models.BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity) =>
            await _DbContect.Set<TEntity>().AddAsync(entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _DbContect.Set<TEntity>().ToListAsync();


        public async Task<TEntity?> GetByIdAsync(Tkey Id)
            => await _DbContect.Set<TEntity>().FindAsync(Id);


        public void Remove(TEntity entity)
             => _DbContect.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
             => _DbContect.Set<TEntity>().Update(entity);

        #region with specifications
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_DbContect.Set<TEntity>(), specifications).ToListAsync();
        }
        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications) 
        {
            return await SpecificationEvaluator.CreateQuery(_DbContect.Set<TEntity>(), specifications).FirstOrDefaultAsync();

        }
        #endregion
    }
}
