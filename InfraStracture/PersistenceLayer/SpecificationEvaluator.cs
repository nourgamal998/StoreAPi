using DomanLayer.Contracts;
using DomanLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace PersistenceLayer
{
    public  static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> Specifications) where TEntity :
            BaseEntity<Tkey>
        {
           var query = inputQuery;
            if (Specifications.Criteria is not null)
            {
                query = query.Where(Specifications.Criteria);
            }
            if (Specifications.IncludeExpressions is not null && Specifications.IncludeExpressions.Count >0)
            {
                query = Specifications.IncludeExpressions
                       .Aggregate(query, (current, IncludeExpressions) => current.Include(IncludeExpressions));
            }
            return query;   
        }

                                     
    }
}

