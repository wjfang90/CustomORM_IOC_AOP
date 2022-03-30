using Custom.Framework.DbFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Model
{
    /// <summary>
    /// 数据库BaseModel
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Id 主键，自动增长
        /// </summary>
        [CustomKey]
        public int Id { get; set; }
    }
}
