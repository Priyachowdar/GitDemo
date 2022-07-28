using System;

namespace Eventcore.Repositories
{
    public class SpecificationBase<T>
    {
        internal object ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}