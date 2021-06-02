using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Configuration
{
    public class SqlServerConfigurationProvider : ConfigurationProvider
    {
        public override void Load()
        {
            Data = GetValues();
        }

        IDictionary<string, string> GetValues()
        {
            const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CONFIGURATION;Trusted_Connection=True;";
            const string Sql = "SELECT [key], [Value] FROM ConfigurationValues";
            IDictionary<string, string> collection;

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                collection = db.Query<KeyValuePair<string, string>>(Sql)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
            }

            return collection;
        }
    }
}
