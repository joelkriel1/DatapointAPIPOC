using DatapointAPIPOC.Db;
using DatapointAPIPOC.Interfaces;
using DatapointAPIPOC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DatapointAPIPOC.Providers
{
    public class NaaProvider : INaaProvider
    {

        private readonly ISnowflakeContext _snowflakeContext;
        private readonly ApplicationDbContext _appDBContext;
        //private readonly ILogger<ProductsProvider> logger;
        //private readonly IMapper mapper;

        public NaaProvider(ISnowflakeContext snowflakeContext, ApplicationDbContext applicationDbContext) //, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _snowflakeContext = snowflakeContext;
            _appDBContext = applicationDbContext;
            //this.logger = logger;
            //this.mapper = mapper;
        }

        public async Task<(bool IsSuccess, TileCard1 TileCard1, string ErrorMessage)> GetTileCard1Async(int WidgetID)
        {

#if DEBUG
            DateTime dt = DateTime.Now;
            Console.WriteLine("START NaaProvider.GetTileCard1Async - " + WidgetID);
#endif

            //List<Widget> widgets = new List<Widget>();
            //UserModel user = _mapper.Map<UserModel>(userModelViewModel);

            //var widgetsWithoutData = await _repo.getWidgetListAsync(user, WidgetID, position).ConfigureAwait(false);

            //List<Widget> widgetsInfo = new List<Widget>();

            //Hard code widget metadata for now
            TileCard1 widget = new TileCard1();
            widget.IsOverrideQuery = true;
            widget.ManuallQuery = @"SELECT VALUE_1, VALUE_3, to_char(ASOFDATE, 'YYYY-MM-DD') as ASOFDATE FROM KPI_LIST WHERE KPI_ID = 2 AND UPPER(UserEmail) = UPPER('$$USEREMAIL$$'); ";
            widget.RegionName = "NAA";
            widget.UserID = "Adam.DiValentino@otis.com";
            widget.WidgetID = WidgetID;
            widget.ElementTemplateID = 7;
            widget.IsRealValues = true;
            //Hard code widget metadata for now


            TileCard1 w = (TileCard1)widget;

            var result = await _snowflakeContext.RawSqlQueryAsync(
                    ((TileCard1)w).QueryToExecute,
                    x => new TileCard1()
                    {
                        Value = Convert.IsDBNull(x[0]) ? 0 : Convert.ToDecimal(x[0]),
                        PerformanceValue = Convert.IsDBNull(x[1]) ? 0 : Convert.ToDecimal(x[1]),
                        AsOfDateValue = (x[2] != null ? (Convert.IsDBNull(x[2]) ? ((DateTime?)null) : DateTime.Parse(x[2])) : null)
                        //AsOfDateValue = (x.FieldCount == 3 ? (Convert.IsDBNull(x[0]) ? ((DateTime?)null) : (DateTime?)x[2]) : null)
                    }
                    , w.RegionName);

            //var result = await _snowflakeContext.RawSqlQueryAsync2(w.QueryToExecute, w.RegionName);

            if (result != null && result.Count() > 0)
            {
                w.Value = result.FirstOrDefault().Value;
                w.PerformanceValue = result.FirstOrDefault().PerformanceValue;
                w.AsOfDateValue = result.FirstOrDefault().AsOfDateValue;
            }

#if DEBUG
            Console.WriteLine("END NaaProvider.GetTileCard1Async - " + WidgetID + " TotalMilliseconds * " + DateTime.Now.Subtract(dt).TotalMilliseconds);
#endif

            return (true, w, null);
        }

        public async Task<(bool IsSuccess, TileCard1 TileCard1, string ErrorMessage)> GetTileCard1Async(TileRequestObject tileRequestObject)
        {

#if DEBUG
            DateTime dt = DateTime.Now;
            Console.WriteLine("START NaaProvider.GetTileCard1Async - " + tileRequestObject.TileId);
#endif

            TileCard1 w = (TileCard1)await getWidgetAsync(tileRequestObject.UserName, int.Parse(tileRequestObject.TileId), 0).ConfigureAwait(false);

            var result = await _snowflakeContext.RawSqlQueryAsync(
                    ((TileCard1)w).QueryToExecute,
                    x => new TileCard1()
                    {
                        Value = Convert.IsDBNull(x[0]) ? 0 : Convert.ToDecimal(x[0]),
                        PerformanceValue = Convert.IsDBNull(x[1]) ? 0 : Convert.ToDecimal(x[1]),
                        AsOfDateValue = (x[2] != null ? (Convert.IsDBNull(x[2]) ? ((DateTime?)null) : DateTime.Parse(x[2])) : null)
                        //AsOfDateValue = (x.FieldCount == 3 ? (Convert.IsDBNull(x[0]) ? ((DateTime?)null) : (DateTime?)x[2]) : null)
                    }
                    , w.RegionName);

            if (result != null && result.Count() > 0)
            {
                w.Value = result.FirstOrDefault().Value;
                w.PerformanceValue = result.FirstOrDefault().PerformanceValue;
                w.AsOfDateValue = result.FirstOrDefault().AsOfDateValue;
            }

#if DEBUG
            Console.WriteLine("END NaaProvider.GetTileCard1Async - " + tileRequestObject.TileId + " TotalMilliseconds * " + DateTime.Now.Subtract(dt).TotalMilliseconds);
#endif

            return (true, w, null);
        }

        public async Task<(bool IsSuccess, TileCard3 TileCard3, string ErrorMessage)> GetTileCard3Async(TileRequestObject tileRequestObject)
        {

            TileCard3 w = (TileCard3)await getWidgetAsync(tileRequestObject.UserName, int.Parse(tileRequestObject.TileId), 0).ConfigureAwait(false);

            var result = await _snowflakeContext.RawSqlQueryAsync(
                    ((TileCard3)w).QueryToExecute,
                    x => new TileCard3()
                    {
                        PlanedValue = Convert.IsDBNull(x[0]) ? 0 : Convert.ToDecimal(x[0]),
                        ActualValue = Convert.IsDBNull(x[1]) ? 0 : Convert.ToDecimal(x[1]),
                        PerformanceValue = Convert.IsDBNull(x[2]) ? 0 : Convert.ToDecimal(x[2]),
                        AsOfDateValue = (x[3] != null ? (Convert.IsDBNull(x[3]) ? ((DateTime?)null) : DateTime.Parse(x[3])) : null)
                        //AsOfDateValue = (x.FieldCount == 4 ? (Convert.IsDBNull(x[0]) ? ((DateTime?)null) : (DateTime?)x[3]) : null)
                    }
                    , w.RegionName);

            if (result != null && result.Count() > 0)
            {
                w.PlanedValue = result.FirstOrDefault().PlanedValue;
                w.ActualValue = result.FirstOrDefault().ActualValue;
                w.PerformanceValue = result.FirstOrDefault().PerformanceValue;
                w.AsOfDateValue = result.FirstOrDefault().AsOfDateValue;
            }

            return (true, w, null);
        }

        public async Task<(bool IsSuccess, TileCard4 TileCard4, string ErrorMessage)> GetTileCard4Async(TileRequestObject tileRequestObject)
        {
            TileCard4 w = (TileCard4)await getWidgetAsync(tileRequestObject.UserName, int.Parse(tileRequestObject.TileId), 0).ConfigureAwait(false);

            var result = await _snowflakeContext.RawSqlQueryAsync(
                    ((TileCard4)w).QueryToExecute,
                    x => new TileCard4()
                    {
                        CurrentValue = Convert.IsDBNull(x[0]) ? 0 : Convert.ToDecimal(x[0]),
                        PendingValue = Convert.IsDBNull(x[1]) ? 0 : Convert.ToDecimal(x[1]),
                        PerformanceValue = Convert.IsDBNull(x[2]) ? 0 : Convert.ToDecimal(x[2]),
                        AsOfDateValue = (x[3] != null ? (Convert.IsDBNull(x[3]) ? ((DateTime?)null) : DateTime.Parse(x[3])) : null)
                        //AsOfDateValue = (x.FieldCount == 4 ? (Convert.IsDBNull(x[3]) ? ((DateTime?)null) : DateTime.Parse(x[3].ToString())) : null)
                    }
                    , w.RegionName);

            if (result != null && result.Count() > 0)
            {
                w.CurrentValue = result.FirstOrDefault().CurrentValue;
                w.PendingValue = result.FirstOrDefault().PendingValue;
                w.PerformanceValue = result.FirstOrDefault().PerformanceValue;
                w.AsOfDateValue = result.FirstOrDefault().AsOfDateValue;
            }

            return (true, w, null);
        }

        public async Task<(bool IsSuccess, TileCard5 TileCard5, string ErrorMessage)> GetTileCard5Async(TileRequestObject tileRequestObject)
        {
            TileCard5 w = (TileCard5)await getWidgetAsync(tileRequestObject.UserName, int.Parse(tileRequestObject.TileId), 0).ConfigureAwait(false);

            var result = await _snowflakeContext.RawSqlQueryAsync(
            ((TileCard5)w).QueryToExecute,
            x => new TileCard5()
            {
                Value1 = Convert.IsDBNull(x[0]) ? 0 : Convert.ToDecimal(x[0]),
                Value2 = Convert.IsDBNull(x[1]) ? 0 : Convert.ToDecimal(x[1]),
                PerformanceValue1 = Convert.IsDBNull(x[2]) ? 0 : Convert.ToDecimal(x[2]),
                PerformanceValue2 = Convert.IsDBNull(x[3]) ? 0 : Convert.ToDecimal(x[3]),
                AsOfDateValue = (x[4] != null ? (Convert.IsDBNull(x[4]) ? ((DateTime?)null) : DateTime.Parse(x[4])) : null)
                        //AsOfDateValue = (x.FieldCount == 5 ? (Convert.IsDBNull(x[4]) ? ((DateTime?)null) : (DateTime?)x[4]) : null)
                    }
            , w.RegionName);

            if (result != null && result.Count() > 0)
            {
                w.Value1 = result.FirstOrDefault().Value1;
                w.Value2 = result.FirstOrDefault().Value2;
                w.PerformanceValue1 = result.FirstOrDefault().PerformanceValue1;
                w.PerformanceValue2 = result.FirstOrDefault().PerformanceValue2;
                w.AsOfDateValue = result.FirstOrDefault().AsOfDateValue;
            }

            return (true, w, null);
        }

        public async Task<(bool IsSuccess, TileCard2 TileCard2, string ErrorMessage)> GetTileCard2Async(TileRequestObject tileRequestObject)
        {
            TileCard2 w = (TileCard2)await getWidgetAsync(tileRequestObject.UserName, int.Parse(tileRequestObject.TileId), 0).ConfigureAwait(false);

            var result = await _snowflakeContext.RawSqlQueryAsync(
                ((TileCard2)w).QueryToExecute,
                x => new TileCard2()
                {
                    Count = Convert.IsDBNull(x[0]) ? 0 : Convert.ToDecimal(x[0]),
                    Amount = Convert.IsDBNull(x[1]) ? 0 : Convert.ToDecimal(x[1]),
                    AsOfDateValue = (x[2] != null ? (Convert.IsDBNull(x[2]) ? ((DateTime?)null) : DateTime.Parse(x[2])) : null)
                            //AsOfDateValue = (x.FieldCount == 3 ? (Convert.IsDBNull(x[0]) ? ((DateTime?)null) : (DateTime?)x[2]) : null)
                        }
                , w.RegionName);
            if (result != null && result.Count() > 0)
            {
                w.Count = result.FirstOrDefault().Count;
                w.Amount = result.FirstOrDefault().Amount;
                w.AsOfDateValue = result.FirstOrDefault().AsOfDateValue;
            }

            return (true, w, null);
        }

        public async Task<(bool IsSuccess, TabledData TabledData, string ErrorMessage)> GetTabledDataAsync(TileRequestObject tileRequestObject)
        {

#if DEBUG
            DateTime dt = DateTime.Now;
            Console.WriteLine("START NaaProvider.GetTileCard1Async - " + tileRequestObject.TileId);
#endif

            TabledData w = (TabledData)await getWidgetAsync(tileRequestObject.UserName, int.Parse(tileRequestObject.TileId), 0).ConfigureAwait(false);


            var result = _snowflakeContext.RawSqlQueryToDataTable(((TabledData)w).QueryToExecute, w.RegionName);

            if (result != null)
            {
                w.Data = result;
                w.AsOfDateValue = DateTime.Now;
            }

#if DEBUG
            Console.WriteLine("END NaaProvider.GetTileCard1Async - " + tileRequestObject.TileId + " TotalMilliseconds * " + DateTime.Now.Subtract(dt).TotalMilliseconds);
#endif

            return (true, w, null);
        }

        public async Task<Widget> getWidgetAsync(string userName, int WidgetID = 0, int position = 0, bool ExecuteQuery = false)
        {
            List<WidgetStructure> Widgets = await _appDBContext.WidgetStructure.Where(m => m.ID == WidgetID && m.IsDeActivated == false).ToListAsync();

            List<Widget> widgetsInfo = new List<Widget>();

            foreach (var widg in Widgets)
            {
                if (widg.ClassType == typeof(TileCard1).ToString() || widg.ClassType == typeof(TileCard1).AssemblyQualifiedName || widg.ClassType.Contains("tilecard1", StringComparison.OrdinalIgnoreCase))
                {
                    TileCard1 w = (TileCard1)PrepareData(widg);
                    w.IsRealValues = true;
                    w.RequiredCaptureValues = false;
                    //w.RoleName = user.Business_Role;
                    w.UserID = userName; // user.UserID;
                    w.WidgetID = widg.ID;

                    widgetsInfo.Add(w);
                }
                else if (widg.ClassType == typeof(TileCard2).ToString() || widg.ClassType == typeof(TileCard2).AssemblyQualifiedName || widg.ClassType.Contains("tilecard2", StringComparison.OrdinalIgnoreCase))
                {
                    TileCard2 w = (TileCard2)PrepareData(widg);
                    w.IsRealValues = true;
                    w.RequiredCaptureValues = false;
                    //w.RoleName = user.Business_Role;
                    w.UserID = userName; // user.UserID;
                    w.WidgetID = widg.ID;

                    widgetsInfo.Add(w);
                }
                else if (widg.ClassType == typeof(TileCard3).ToString() || widg.ClassType == typeof(TileCard3).AssemblyQualifiedName || widg.ClassType.Contains("tilecard3", StringComparison.OrdinalIgnoreCase))
                {
                    TileCard3 w = (TileCard3)PrepareData(widg);
                    w.IsRealValues = true;
                    w.RequiredCaptureValues = false;
                    //w.RoleName = user.Business_Role;
                    w.UserID = userName; // user.UserID;
                    w.WidgetID = widg.ID;

                    widgetsInfo.Add(w);
                }
                else if (widg.ClassType == typeof(TileCard4).ToString() || widg.ClassType == typeof(TileCard4).AssemblyQualifiedName || widg.ClassType.Contains("tilecard4", StringComparison.OrdinalIgnoreCase))
                {
                    TileCard4 w = (TileCard4)PrepareData(widg);
                    w.IsRealValues = true;
                    w.RequiredCaptureValues = false;
                    //w.RoleName = user.Business_Role;
                    w.UserID = userName; // user.UserID;
                    w.WidgetID = widg.ID;

                    widgetsInfo.Add(w);
                }
                else if (widg.ClassType == typeof(TileCard5).ToString() || widg.ClassType == typeof(TileCard5).AssemblyQualifiedName || widg.ClassType.Contains("tilecard5", StringComparison.OrdinalIgnoreCase))
                {
                    TileCard5 w = (TileCard5)PrepareData(widg);
                    w.IsRealValues = true;
                    w.RequiredCaptureValues = false;
                    //w.RoleName = user.Business_Role;
                    w.UserID = userName; // user.UserID;
                    w.WidgetID = widg.ID;

                    widgetsInfo.Add(w);
                }
                else if (widg.ClassType == typeof(TabledData).ToString() || widg.ClassType == typeof(TabledData).AssemblyQualifiedName || widg.ClassType.Contains("tableddata", StringComparison.OrdinalIgnoreCase))
                {
                    TabledData w = (TabledData)PrepareData(widg); 
                    w.IsRealValues = true;
                    w.RequiredCaptureValues = false;
                    //w.RoleName = user.Business_Role;
                    w.UserID = userName; // user.UserID;
                    w.WidgetID = widg.ID;

                    widgetsInfo.Add(w);
                }
                //else if (widg.ClassType == typeof(ChartData).ToString() || widg.ClassType == typeof(ChartData).AssemblyQualifiedName || widg.ClassType.Contains("chartdata", StringComparison.OrdinalIgnoreCase))
                //{
                //    ChartData w = (ChartData)PrepareData(widg);
                //    w.IsRealValues = true;
                //    w.RequiredCaptureValues = false;
                //    w.RoleName = user.Business_Role;
                //    w.UserID = user.UserID;

                //    if (ExecuteQuery)
                //    {
                //        var result = _appDBContext.RawSqlQueryToDataTable(((ChartData)w).QueryToExecute);
                //        if (result != null)
                //        {
                //            w.Data = result;
                //            w.AsOfDateValue = DateTime.Now;
                //        }
                //    }
                //    w.WidgetID = widg.ID;

                //    widgetsInfo.Add(w);
                //}
                //else if (widg.ClassType == typeof(PieChart).ToString() || widg.ClassType == typeof(PieChart).AssemblyQualifiedName || widg.ClassType.Contains("PieChart", StringComparison.OrdinalIgnoreCase))
                //{
                //    PieChart w = (PieChart)PrepareData(widg);
                //    w.IsRealValues = true;
                //    w.RequiredCaptureValues = false;
                //    w.RoleName = user.Business_Role;
                //    w.UserID = user.UserID;

                //    if (ExecuteQuery)
                //    {
                //        var result = await _appDBContext.RawSqlQueryAsync(
                //        ((PieChart)w).QueryToExecute,
                //        x => new PieChart() { Category = x[0].ToString(), Value = (x[1]).ToString(), AsOfDateValue = (x.FieldCount == 3 ? (DateTime?)x[2] : null) }
                //        );

                //        if (result != null && result.Count() > 0)
                //        {
                //            var records = from record in result select new PieChartRecord() { Category = record.Category, Value = record.Value };
                //            w.Data = records;
                //            w.AsOfDateValue = result.FirstOrDefault().AsOfDateValue;
                //        }
                //    }

                //    w.WidgetID = widg.ID;

                //    widgetsInfo.Add(w);
                //}
            }

            if (widgetsInfo != null)
            {
                widgetsInfo.ForEach(m => m.Position = position);
            }

            return widgetsInfo[0];
        }

        private object PrepareData(WidgetStructure obj)
        {
            Type ObjInstance = GetTypeByElementId(obj.ElementID); //Type.GetType(obj.ClassType);

            var WidgetObject = JsonSerializer.Deserialize(obj.Formation, ObjInstance);

            return WidgetObject;
        }

        private static Type GetTypeByElementId(int ID)
        {
            Type T = null;
            switch (ID)
            {
                case 7:
                    T = typeof(TileCard1);
                    break;
                case 8:
                    T = typeof(TileCard4);
                    break;
                case 9:
                    T = typeof(TileCard5);
                    break;
                case 10:
                    T = typeof(TabledData);
                    break;
                //case 11:
                //case 12:
                //case 13:
                //    T = typeof(ChartData);
                //    break;
                case 6:
                    T = typeof(TileCard2);
                    break;
                //case 5:
                //case 4:
                //case 3:
                //    T = typeof(PieChart);
                //    break;
                case 2:
                    T = typeof(TileCard3);
                    break;
            }
            return T;
        }

    }
}
