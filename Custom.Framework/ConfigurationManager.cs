using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Custom.Framework
{
    public class ConfigurationManager
    {
        private static string sqlConnectionStrings;
        private static string sqlConnectionStringWrite;
        private static string[] sqlConnectionStringReads;
        public static string SqlConnectionsStrings => sqlConnectionStrings;

        public static string SqlConnectionStringWrite => sqlConnectionStringWrite;
        public static string[] SqlConnectionStringReads => sqlConnectionStringReads;

        static ConfigurationManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            sqlConnectionStrings = config.GetSection("ConnectionStrings:Customers").Value;
            sqlConnectionStringWrite = config.GetSection("ConnectionStringsReadWrite:Write").Value;
            sqlConnectionStringReads = config.GetSection("ConnectionStringsReadWrite:Read").GetChildren().Select(t => t.Value).ToArray();
        }
    }
}
