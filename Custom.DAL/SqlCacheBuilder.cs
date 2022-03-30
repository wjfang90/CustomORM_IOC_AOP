using Custom.Framework.DbFilters;
using Custom.Framework.Mapping;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom.DAL
{
    public class SqlCacheBuilder<T> where T : BaseModel, new()
    {
        private static string getOneSql;
        private static string getListSql;
        private static string insertSql;
        private static string updateSql;
        private static string deleteSql;
        static SqlCacheBuilder()
        {

            //var tableName = typeof(T).Name;//数据库必须与Model同名
            var tableName = typeof(T).GetMappingName();
            var keyName = typeof(T).GetKeyName();

            #region GetOne
            {
                //var propNames = string.Join(",", typeof(T).GetProperties().Select(t => t.Name));//字段名必须与数据库同名
                var propNames = string.Join(",", typeof(T).GetProperties().Select(t => $"[{ t.GetMappingName()}]"));//优化字段名与数据库同名的问题
                getOneSql = $"select {propNames} from [{tableName}] where {keyName}=@{keyName}";
            }
            #endregion

            #region GetList
            {
                var propNames = string.Join(",", typeof(T).GetProperties().Select(t => $"[{ t.GetMappingName()}]"));//优化字段名与数据库同名的问题
                //getListSql = $"select top 10 {propNames} from [{tableName}] where account like @account and state=@state or @account like @account";
                getListSql = $"select top 10 {propNames} from [{tableName}]";
            }
            #endregion

            #region Insert
            {
                var propNames = string.Join(",", typeof(T).GetPropertiesNoKey().Select(t => $"[{ t.GetMappingName()}]"));
                var paramNames = string.Join(",", typeof(T).GetPropertiesNoKey().Select(t => $"@{ t.GetMappingName()}"));

                insertSql = $"insert into [{tableName}]({propNames}) values({paramNames})";
            }
            #endregion

            #region Update
            {
                var proNameValues = string.Join(",", typeof(T).GetPropertiesNoKey().Select(t => $"[{t.GetMappingName()}] = @{t.GetMappingName()}"));
                updateSql = $"update [{tableName}] set {proNameValues} where {keyName}=@{keyName}";
            }
            #endregion

            #region Delete

            {
                deleteSql = $"delete from [{tableName}] where {keyName}=@{keyName}";
            }

            #endregion
        }

        public static string GetSql(SqlCacheType sqlCacheType)
        {
            switch (sqlCacheType)
            {
                case SqlCacheType.GetOne:
                    return getOneSql;
                case SqlCacheType.GetList:
                    return getListSql;
                case SqlCacheType.Insert:
                    return insertSql;
                case SqlCacheType.Update:
                    return updateSql;
                case SqlCacheType.Delete:
                    return deleteSql;
                default:
                    return null;
            }


        }
    }

    public enum SqlCacheType
    {
        GetOne,
        GetList,
        Insert,
        Update,
        Delete
    }
}
