using DatapointAPIPOC.Classes;
using System.Collections.Generic;
using System.Data;

namespace DatapointAPIPOC.Models
{
    public class TabledData : Widget
    {
        public List<string> ColumnNames { get; set; }
        public DataTable Data { get; set; }

        public string SelectedColumns
        {
            get
            {
                return ColumnNames != null && ColumnNames.Count > 0 ? string.Join(",", ColumnNames) : "";
            }
        }


        public string FilterColumnName { get; set; }
        public string Filter { get; set; }



        public string Query
        {
            get
            {
                string Query = "SELECT " + SelectedColumns + (string.IsNullOrWhiteSpace(base.AsOfDateColumnName) ? "" : ((", ") + base.AsOfDateColumnName)) + " from " + base.SourceName;
                bool isAnd = false;
                if (!string.IsNullOrEmpty(FilterColumnName) && !string.IsNullOrEmpty(Filter))
                {
                    Query = Query + (!isAnd ? " where " : " and ") + FilterColumnName + " ='" + Filter + "'";
                    isAnd = true;
                }
                if (base.isFilteredByUserId)
                {
                    Query = Query + (!isAnd ? " where upper(UserEmail) = '" : " and upper(UserEmail) = '") + base.UserID.ToStr().ToUpper() + "'";
                    isAnd = true;
                }
                if (base.isFilteredByRole)
                {
                    Query = Query + (!isAnd ? " where upper(RoleName) = '" : " and upper(RoleName) = '") + base.RoleName.ToStr().ToUpper() + "'";
                    isAnd = true;
                }
                return Query;
            }
        }

        public string QueryToExecute
        {
            get
            {
                string Query = (base.IsOverrideQuery ? base.ManuallQuery : this.Query).ToUpper();

                Query = Query.ToStr().Replace("$$USEREMAIL$$", base.UserID.ToStr().ToUpper());
                Query = Query.ToStr().Replace("$$ROLENAME$$", base.RoleName.ToStr().ToUpper());

                return Query;

            }
        }

    }
}
