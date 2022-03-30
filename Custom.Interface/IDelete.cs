using Custom.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Interface
{
    public interface IDelete<T> where T : BaseModel, new()
    {
        public bool Delete(T model);
    }
}
