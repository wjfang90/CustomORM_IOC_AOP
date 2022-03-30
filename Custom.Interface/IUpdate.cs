using Custom.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Interface
{
    public interface IUpdate<T> where T : BaseModel, new()
    {
        public bool Update(T model);
    }
}
