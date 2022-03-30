﻿using Custom.Framework.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.Model
{
    /// <summary>
    /// 一律在实体后面加个model---CompanyModel
    /// 改了数据库表，不想改代码
    /// 实体跟数据库的表&字段，不完全一致
    /// 
    /// </summary>
    [CustomTableName("Company")]
    public class CompanyModel : BaseModel
    {

        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        public Nullable<int> LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }
    }
}
