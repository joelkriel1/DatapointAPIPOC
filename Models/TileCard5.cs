using DatapointAPIPOC.Classes;

namespace DatapointAPIPOC.Models
{
    public class TileCard5 : Widget
    {
        /// <summary>
        /// Actual Peroperties that holds the values
        /// </summary>
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal PerformanceValue1 { get; set; }
        public decimal PerformanceValue2 { get; set; }

        public string Value1Format { get; set; }
        public string Value2Format { get; set; }
        public string PerformanceValue1Format { get; set; }
        public string PerformanceValue2Format { get; set; }

        public string Value1Link { get; set; }
        public string Value2Link { get; set; }


        /// <summary>
        /// Peropeties that holds the above values columns Name
        /// </summary>
        public string Value1ColumnName { get; set; }
        public string Value2ColumnName { get; set; }
        public string PerformanceValue1ColumnName { get; set; }
        public string PerformanceValue2ColumnName { get; set; }



        public string Value1LabelName { get; set; }
        public string Value2LabelName { get; set; }


        public string FilterColumnName { get; set; }
        public string Filter { get; set; }



        public string Query
        {
            get
            {
                string Query = "SELECT top 1 " + Value1ColumnName + ", " + Value2ColumnName + ", " + PerformanceValue1ColumnName + ", " + PerformanceValue2ColumnName + (string.IsNullOrWhiteSpace(base.AsOfDateColumnName) ? "" : ((", ") + base.AsOfDateColumnName)) + " from " + base.SourceName;
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
