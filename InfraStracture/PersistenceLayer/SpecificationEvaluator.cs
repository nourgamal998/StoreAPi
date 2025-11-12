using DomanLayer.Contracts;
using DomanLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace PersistenceLayer
{
    internal  static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> Specifications) where TEntity :
            BaseEntity<Tkey>

        {
           var query = inputQuery;
            if (Specifications.Criteria is not null)
                query = query.Where(Specifications.Criteria);
            #region order 
            if (Specifications.OrderBy is not null)
             query = query.OrderBy(Specifications.OrderBy);
            

            if (Specifications.OrderByDescending is not null)
                query = query.OrderByDescending(Specifications.OrderByDescending);
            #endregion

            #region include


            if (Specifications.IncludeExpressions is not null && Specifications.IncludeExpressions.Count >0)
                query = Specifications.IncludeExpressions.Aggregate(query, (current, IncludeExpressions) 
                                                                 => current.Include(IncludeExpressions));

            #endregion

            #region pagination

             if (Specifications.IsPaginated)
                query = query.Skip(Specifications.Skip).Take(Specifications.Take);


            #endregion


            return query;
        }


    }
}

