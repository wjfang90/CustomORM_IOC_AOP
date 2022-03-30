using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Custom.Interface
{
    public interface IQuery<T> where T : BaseModel, new()
    {
        public T Find(int id);

        public IEnumerable<T> Find(Expression<Func<T, bool>> exp) ;
    }
}
