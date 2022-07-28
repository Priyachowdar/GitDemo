using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eventcore.Specifications
{
    public abstract class SpecificationBase<T> where T : class
    {
        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
