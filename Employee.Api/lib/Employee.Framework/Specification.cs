using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Employee.Framework
{
    public abstract class Specification<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
