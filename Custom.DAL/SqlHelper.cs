using Custom.DAL.ExpressionToSql;
using Custom.Framework;
using Custom.Framework.DbFilters;
using Custom.Framework.Mapping;
using Custom.Framework.Validate;
using Custom.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Custom.DAL
{
    public class SqlHelper
    {

        #region dal 

        public static T Find<T>(int id) where T : BaseModel, new()
        {
            /*
            //优化使用泛型缓存
            //var propNames = string.Join(",", typeof(T).GetProperties().Select(t => t.Name));//字段名必须与数据库同名
            var propNames = string.Join(",", typeof(T).GetProperties().Select(t => $"[{ t.GetMappingName()}]"));//优化字段名与数据库同名的问题
            //var tableName = typeof(T).Name;//数据库必须与Model同名
            var tableName = typeof(T).GetMappingName();//优化数据库与Model同名的问题
            var sql = $"select {propNames} from [{tableName}] where Id=@Id";
            */

            //优化使用泛型缓存
            var sql = SqlCacheBuilder<T>.GetSql(SqlCacheType.GetOne);
            var keyName = typeof(T).GetKeyName();

            //return ExecuteSql<T>(sql, new SqlParameter($"@{keyName}", id));//优化
            return ExecuteSql<T>(sql,
                    cmd =>
                    {
                        var dataReader = cmd.ExecuteReader();
                        if (dataReader == null || !dataReader.Read())
                        {
                            return null;
                        }

                        var model = new T();
                        foreach (var prop in typeof(T).GetProperties())
                        {
                            //prop.SetValue(model, dataReader[prop.Name] is DBNull ? null : dataReader[prop.Name]);//优化字段名与数据库同名的问题
                            prop.SetValue(model, dataReader[prop.GetMappingName()] is DBNull ? null : dataReader[prop.GetMappingName()]);
                        };
                        return model;
                    },
                    SqlConnectionType.Read,
                    new SqlParameter($"@{keyName}", id));
        }

        public static bool Insert<T>(T model) where T : BaseModel, new()
        {
            if (model == null) return false;

            if (!model.ValidateData())
            {
                throw new Exception("数据验证失败！");
            }

            /*
            //优化使用泛型缓存
            var tableName = typeof(T).GetMappingName();
            var propNames = string.Join(",", typeof(T).GetPropertiesNoKey().Select(t => $"[{ t.GetMappingName()}]"));
            var paramNames = string.Join(",", typeof(T).GetPropertiesNoKey().Select(t => $"@{ t.GetMappingName()}"));

            var sql = $"insert into [{tableName}]({propNames}) values({paramNames})";
            */

            //优化使用泛型缓存
            var sql = SqlCacheBuilder<T>.GetSql(SqlCacheType.Insert);

            var paramValus = typeof(T).GetPropertiesNoKey().Select(t => new SqlParameter($"@{t.GetMappingName()}", t.GetValue(model) ?? DBNull.Value)).ToArray();

            var result = ExecuteNoQuery(sql, paramValus);
            return result == 1;
        }

        public static bool Update<T>(T model) where T : BaseModel, new()
        {
            if (model == null) return false;

            if (!model.ValidateData())
            {
                throw new Exception("数据验证失败！");
            }

            //优化使用泛型缓存
            var sql = SqlCacheBuilder<T>.GetSql(SqlCacheType.Update);

            var paramValus = typeof(T).GetPropertiesNoKey().Select(t => new SqlParameter($"@{t.GetMappingName()}", t.GetValue(model) ?? DBNull.Value)).ToArray();

            var keyName = typeof(T).GetKeyName();
            var keyValue = typeof(T).GetProperty(keyName).GetValue(model) ?? DBNull.Value;
            paramValus = paramValus.Append(new SqlParameter($"@{keyName}", keyValue)).ToArray();

            var result = ExecuteNoQuery(sql, paramValus);
            return result == 1;
        }

        public static bool Delete<T>(T model) where T : BaseModel, new()
        {
            if (model == null) return false;

            //优化使用泛型缓存
            var sql = SqlCacheBuilder<T>.GetSql(SqlCacheType.Delete);

            var keyName = typeof(T).GetKeyName();
            var keyValue = typeof(T).GetProperty(keyName).GetValue(model) ?? DBNull.Value;
            var paramValus = new SqlParameter($"@{keyName}", keyValue);

            var result = ExecuteNoQuery(sql, paramValus);
            return result == 1;
        }


        public static IEnumerable<T> Find<T>(Expression<Func<T, bool>> exp) where T : BaseModel, new()
        {
            //优化使用泛型缓存
            var sql = SqlCacheBuilder<T>.GetSql(SqlCacheType.GetList);

            //参数化查询中的 sqlparameter 与sql语句中的参数顺序无关，相当于指定参数名不同
            //fang do 一个参数多条件匹配问题
            //  account like '%f' or account like 'g%'    ==> account like @account1 or account like @account2  ;  @account1='%f',@account2='g%'
            var expToSql = new ExpressionToSqlExtend<T>();
            expToSql.Init(exp);
            sql += expToSql.GetWhere();
            var parameters = expToSql.GetDbParameter();

            return ExecuteSql<IEnumerable<T>>(sql,
                    cmd =>
                    {
                        var dataTable = new DataTable();
                        var dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);

                        if (dataTable == null || dataTable.Rows.Count == 0)
                        {
                            return null;
                        }

                        var list = new List<T>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            var model = new T();
                            foreach (var prop in typeof(T).GetProperties())
                            {
                                prop.SetValue(model, row[prop.GetMappingName()] is DBNull ? null : row[prop.GetMappingName()]);
                            };
                            list.Add(model);
                        }

                        return list;
                    },
                    SqlConnectionType.Read,
                    parameters.ToArray());
        }

        #endregion


        #region ado.net 操作
        /// <summary>
        /// 待优化代码--只查一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        //private static T ExecuteSql<T>(string sql, params SqlParameter[] parameters) where T : BaseModel, new()
        //{
        //    using (SqlConnection conn = new SqlConnection(ConfigurationManager.SqlConnectionsStrings))
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.AddRange(parameters);
        //        conn.Open();

        //        //cmd.ExecuteNonQuery();
        //        var dataReader = cmd.ExecuteReader();
        //        if (dataReader != null && dataReader.Read())
        //        {
        //            var model = new T();

        //            foreach (var prop in typeof(T).GetProperties())
        //            {
        //                //prop.SetValue(model, dataReader[prop.Name] is DBNull ? null : dataReader[prop.Name]);//优化字段名与数据库同名的问题
        //                prop.SetValue(model, dataReader[prop.GetMappingName()] is DBNull ? null : dataReader[prop.GetMappingName()]);
        //            };
        //            return model;
        //        }
        //    }
        //    return null;
        //}

        /// <summary>
        /// 已优化代码
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="sql"></param>
        /// <param name="func"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static S ExecuteSql<S>(string sql, Func<SqlCommand, S> func, SqlConnectionType sqlConnectionType = SqlConnectionType.Read, params SqlParameter[] parameters)
        {
            var connString = SqlConnectionStringPool.GetConnectinString(sqlConnectionType);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters);
                conn.Open();

                return func.Invoke(cmd);
            }
        }

        private static int ExecuteNoQuery(string sql, params SqlParameter[] parameters)
        {
            var connString = SqlConnectionStringPool.GetConnectinString(SqlConnectionType.Write);
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters);
                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
