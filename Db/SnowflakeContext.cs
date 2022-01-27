using DatapointAPIPOC.Interfaces;
using DatapointAPIPOC.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Snowflake.Data.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DatapointAPIPOC.Db
{
    public class SnowflakeContext : ISnowflakeContext
    {
        //private readonly string connectionString;
        //private readonly AppsettingsConnectionStrings _mySettings;
        private readonly IConfiguration Configuration;
        ///<summary>
        /// Initializes a new instance of the SnowflakeClient class.
        ///</summary>
        public SnowflakeContext(IConfiguration configuration) //IOptions<AppsettingsConnectionStrings> settings, IConfiguration configuration)
        {
            //_mySettings = settings.Value;
            //this.connectionString = _mySettings.SnowflakeDbConnection;
            Configuration = configuration;
        }

        public async Task<List<T>> RawSqlQueryAsync<T>(string query, Func<List<string>, T> map, string RegionName)
        {
            var configSection = Configuration.GetSection("Multitenancy:SnowflakeTenants").Get<List<Tenant>>();
            var tenant = configSection.Where(s => s.RequestPath.Contains(RegionName)).FirstOrDefault();

            var _snowflakeClient = new Snowflake.Client.SnowflakeClient(
                new Snowflake.Client.Model.AuthInfo(tenant.User, tenant.Password, tenant.Account, tenant.Region),
                new Snowflake.Client.Model.SessionInfo
                {
                    Role = tenant.Role,
                    Database = tenant.Database,
                    Schema = tenant.Schema,
                    Warehouse = tenant.Warehouse
                });

            var entities = new List<T>();
            // Executes query and returns raw response from Snowflake (rows, columns and query information)
            var queryDataResponse = await _snowflakeClient.QueryRawResponseAsync(query);

            foreach (var item in queryDataResponse.Rows)
            {
                entities.Add(map(item));
            }

            return entities;
        }

        public DataTable RawSqlQueryToDataTable(string query, string RegionName)
        {
            var conn = new SnowflakeDbConnection();
            var configSection = Configuration.GetSection("Multitenancy:SnowflakeTenants").Get<List<Tenant>>();
            var tenant = configSection.Where(s => s.RequestPath.Contains(RegionName)).FirstOrDefault();
            conn.ConnectionString = tenant.ConnectionString;

            DataTable table = null;

            using (var command = conn.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                conn.Open();

                using (var result = command.ExecuteReader())
                {
                    //Create a new DataTable.
                    table = new DataTable("Table");

                    //Load DataReader into the DataTable.
                    table.Load(result);
                }
            }
            return table;
        }

        public async Task<List<TileCard1>> RawSqlQueryAsync2(string query, string RegionName)
        {
#if DEBUG
            DateTime dt = DateTime.Now;
            Console.WriteLine("START SnowflakeContext.RawSqlQueryAsync - ");
#endif
            var configSection = Configuration.GetSection("Multitenancy:SnowflakeTenants").Get<List<Tenant>>();
            var tenant = configSection.Where(s => s.RequestPath.Contains(RegionName)).FirstOrDefault();

            var _snowflakeClient = new Snowflake.Client.SnowflakeClient(
                new Snowflake.Client.Model.AuthInfo(tenant.User, tenant.Password, tenant.Account, tenant.Region),
                new Snowflake.Client.Model.SessionInfo
                {
                    Role = tenant.Role,
                    Database = tenant.Database,
                    Schema = tenant.Schema,
                    Warehouse = tenant.Warehouse
                });
#if DEBUG
            Console.WriteLine("SnowflakeContext._snowflakeClient - " + " TotalMilliseconds * " + DateTime.Now.Subtract(dt).TotalMilliseconds);
#endif

            //var entities = new List<T>();

            // Executes query and returns raw response from Snowflake (rows, columns and query information)
            var queryDataResponse = await _snowflakeClient.QueryAsync<TileCard1>(query);

#if DEBUG
            Console.WriteLine("SnowflakeContext.QueryRawResponseAsync - " + " TotalMilliseconds * " + DateTime.Now.Subtract(dt).TotalMilliseconds);
#endif

            return queryDataResponse.ToList();
        }

    }
}
