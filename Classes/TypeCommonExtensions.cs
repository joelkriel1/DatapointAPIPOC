using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace DatapointAPIPOC.Classes
{
    public static class TypeCommonExtensions
    {
        public static int ToInt(this object str)
        {
            if (str == DBNull.Value || str == null)
            {
                return 0;
            }
            else
            {
                int retValue;
                int.TryParse(str.ToString(), out retValue);
                return retValue;
            }
        }

        public static bool ToBoolean(this object val)
        {
            return Convert.ToBoolean(val);
        }

        public static byte ToByte(this object str)
        {
            if (str == DBNull.Value || str == null)
            {
                return 0;
            }
            else
            {
                byte retValue;
                byte.TryParse(str.ToString(), out retValue);
                return retValue;
            }
        }

        public static DateTime ToDateTime(this object str)
        {
            if (str == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            else
            {
                DateTime retValue;
                DateTime.TryParse(str.ToString(), out retValue);
                return retValue;
            }
        }

        public static DateTime? NullOrToDateTime(this object str)
        {
            try
            {
                if (str == DBNull.Value || str == null)
                {
                    return null;
                }
                else
                {
                    DateTime retValue;
                    DateTime.TryParse(str.ToString(), out retValue);
                    if (retValue == DateTime.MinValue)
                    {
                        return null;
                    }
                    return retValue;
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool IsNumeric(this object str)
        {
            double retValue;
            return double.TryParse(str.ToString(), out retValue);
        }

        public static char ToChar(this string str)
        {
            char retValue;
            char.TryParse(str, out retValue);
            return retValue;
        }

        public static double ToDouble(this object obj)
        {
            if (obj == DBNull.Value || obj == null)
            {
                return 0.0;
            }
            else
            {
                double retValue;
                double.TryParse(obj.ToString(), out retValue);
                return retValue;
            }
        }

        public static decimal ToDecimal(this object str)
        {
            if (str == DBNull.Value || str == null)
            {
                return 0;
            }
            else
            {
                decimal retValue;
                decimal.TryParse(str.ToString(), out retValue);
                return retValue;
            }
        }

        public static long ToLong(this object str)
        {
            if (str == DBNull.Value || str == null)
            {
                return 0;
            }
            else
            {
                long retValue;
                long.TryParse(str.ToString(), out retValue);
                return retValue;
            }
        }

        public static DateTime IsDateTime(this string str)
        {
            DateTime retValue;
            DateTime.TryParse(str, out retValue);
            return retValue;
        }

        public static string ToStr(this object val, bool returnEmptyIfDBNull = true)
        {
            if (val == DBNull.Value)
                return returnEmptyIfDBNull ? string.Empty : null;
            if (val == null)
                return string.Empty;
            else
            {
                try
                {
                    return val.ToString().Trim();
                }
                catch
                {
                    return val.ToString();
                }
            }
        }

        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                table.Rows.Add(values);
            }
            return table;
        }

        public static string ToShortNumber(this decimal number)
        {
            int num = number.ToInt();

            if (num < 1000)
            {
                return number.ToString("#0", CultureInfo.CurrentCulture);
            }

            if (num < 10000)
            {
                num /= 10;
                return (num / 100f).ToString("#.00'K'", CultureInfo.CurrentCulture);
            }

            if (num < 1000000)
            {
                num /= 100;
                return (num / 10f).ToString("#.0'K'", CultureInfo.CurrentCulture);
            }

            if (num < 10000000)
            {
                num /= 10000;
                return (num / 100f).ToString("#.00'M'", CultureInfo.CurrentCulture);
            }

            num /= 100000;
            return (num / 10f).ToString("#,0.0'M'", CultureInfo.CurrentCulture);
        }
    }
}
