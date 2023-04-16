using Demo.BLL.Entities;
using Demo.BLL.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputquery, ISpecification<TEntity> Spec)
        {
            var query = inputquery;

            if (Spec.Critenia != null)
                query = query.Where(Spec.Critenia);
            if(Spec.OrderBy !=null) 
                query = query.OrderBy(Spec.OrderBy);
            if(Spec.OrderByDescinding !=null)
                query=query.OrderByDescending(Spec.OrderByDescinding);
            //  query = Spec.Includes.Aggregate(query, (Current, include) => Current.Include(include));
            //query = Spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            query = Spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
