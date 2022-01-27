
using DatapointAPIPOC.Classes;

namespace DatapointAPIPOC.Models
{
    public class TileCard4 : Widget
    {
        /// <summary>
        /// Actual Peroperties that holds the values
        /// </summary>
        public decimal CurrentValue { get; set; }
        public decimal PendingValue { get; set; }
        public decimal PerformanceValue { get; set; }

        public string CurrentValueFormat { get; set; }
        public string PendingValueFormat { get; set; }
        public string PerformanceValueFormat { get; set; }

        public string CurrentValueLink { get; set; }
        public string PendingValueLink { get; set; }


        /// <summary>
        /// Peropeties that holds the above values columns Name
        /// </summary>
        public string CurrentValueColumnName { get; set; }
        public string PendingValueColumnName { get; set; }
        public string PerformanceValueColumnName { get; set; }






        public string FilterColumnName { get; set; }
        public string Filter { get; set; }

        public string Query
        {
            get
            {
                string Query = "SELECT top 1 " + CurrentValueColumnName + ", " + PendingValueColumnName + ", " + PerformanceValueColumnName + (string.IsNullOrWhiteSpace(base.AsOfDateColumnName) ? "" : ((", ") + base.AsOfDateColumnName)) + " from " + base.SourceName;
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
