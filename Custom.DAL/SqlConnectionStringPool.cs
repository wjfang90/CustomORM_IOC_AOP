using Custom.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custom.DAL
{
    public class SqlConnectionStringPool
    {
        private static int readIndex;

        public static string GetConnectinString(SqlConnectionType sqlConnectionType)
        {
            switch (sqlConnectionType)
            {
                case SqlConnectionType.Read:
                    {
                        readIndex = ++readIndex >= int.MaxValue ? 0 : readIndex;
                        return ConfigurationManager.SqlConnectionStringReads[readIndex % ConfigurationManager.SqlConnectionStringReads.Length];//默认是轮询模式
                    }
                    
                case SqlConnectionType.Write:
                    return ConfigurationManager.SqlConnectionStringWrite;
                default:
                    return ConfigurationManager.SqlConnectionsStrings;
            }
        }
    }

    public enum SqlConnectionType
    {
        Read,
        Write
    }
}
