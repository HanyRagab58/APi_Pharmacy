using Demo.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criterie)
        {
            Critenia = criterie;
        }

        //public BaseSpecification()
        //{
            
        //}

        public Expression<Func<T, bool>> Critenia { get; }

        public List<Expression<Func<T, object>>> Includes { get; } =new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescinding { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IspagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> include) 
        {
            Includes.Add(include);
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderby)
        {
            OrderBy = orderby;
        }
        protected void AddOrderByDescinding(Expression<Func<T, object>> orderbyDescinding)
        {
            OrderByDescinding = orderbyDescinding;
        }
        protected void ApplyPaging(int skip, int take) 
        {
            Skip = skip;
            Take = take;
            
            IspagingEnabled= true;
        }

    }
}
