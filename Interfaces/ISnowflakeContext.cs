using DatapointAPIPOC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DatapointAPIPOC.Interfaces
{
    public interface ISnowflakeContext
    {
        Task<List<T>> RawSqlQueryAsync<T>(string query, Func<List<string>, T> map, string RegionName);
        Task<List<TileCard1>> RawSqlQueryAsync2(string query, string RegionName);
        DataTable RawSqlQueryToDataTable(string query, string RegionName);

    }
}
