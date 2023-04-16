﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Specification
{
    public interface ISpecification<T>
    {
        Expression <Func< T, bool>> Critenia { get; }
        List<Expression<Func< T, object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescinding { get; }
        int Take { get; }
        int Skip { get; }
        bool IspagingEnabled { get; }
    }
}