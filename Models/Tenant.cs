using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatapointAPIPOC.Models
{
    public class Tenant
    {
        public string RequestPath { get; set; }
        public string ConnectionString { get; set; }
        public string Domain { get; set; }
        public string Environment { get; set; }
        public string Schema { get; set; }
        public string Account { get; set; }
        public string Host { get; set; }
        public string Region { get; set; }
        public string Role { get; set; }
        public string Warehouse { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        /*        "Account": "otiselevator",
        "Host": "otiselevator.east-us-2.azure.snowflakecomputing.com",
        "Role": "OTIS-DL-SF-DATAPOINT-DEVELOPER",
        "Warehouse": "DATAPOINT_NAA_DEV_WH",
        "User": "SVC-DATAPOINT-USER",
        "Password": "Datapoint@123",
        "Database": "DATAPOINT_DEV" */
    }
}
