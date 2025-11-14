using DomanLayer.Models;

namespace DomanLayer.Contracts
{
    public interface IGenericRepository <TEntity , Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Tkey Id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        #region with specifications

        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity,Tkey> specifications);

        Task<IEnumerable<TEntity>> GetAsync(ISpecifications<TEntity, Tkey> specifications);

        Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications);
       

        #endregion
    }
}
