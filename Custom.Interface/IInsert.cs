using Custom.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Interface
{
    public interface IInsert<T> where T : BaseModel, new()
    {
        public bool Insert(T model) ;
    }
}
