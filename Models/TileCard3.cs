using DatapointAPIPOC.Classes;

namespace DatapointAPIPOC.Models
{
    public class TileCard3 : Widget
    {
        /// <summary>
        /// Actual Peroperties that holds the values
        /// </summary>
        public decimal PlanedValue { get; set; }
        public decimal ActualValue { get; set; }
        public decimal PerformanceValue { get; set; }

        public string PlanedValueFormat { get; set; }
        public string ActualValueFormat { get; set; }
        public string PerformanceValueFormat { get; set; }

        public string ActualValueLink { get; set; }


        /// <summary>
        /// Peropeties that holds the above values columns Name
        /// </summary>
        public string PlanedValueColumnName { get; set; }
        public string ActualValueColumnName { get; set; }
        public string PerformanceValueColumnName { get; set; }
        public string FilterColumnName { get; set; }
        public string Filter { get; set; }

        public string Query
        {
            get
            {
                string Query = "SELECT top 1 " + PlanedValueColumnName + ", " + ActualValueColumnName + ", " + PerformanceValueColumnName + (string.IsNullOrWhiteSpace(base.AsOfDateColumnName) ? "" : ((", ") + base.AsOfDateColumnName)) + " from " + base.SourceName;
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
