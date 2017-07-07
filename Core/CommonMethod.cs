using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core
{
    public static class CommonMethod
    {
        /// <summary>
        /// Convert DataTable to List<T>/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datatable"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(this DataTable datatable) where T : new()
        {
            List<T> Temp = new List<T>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                Temp = datatable.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnsNames));
                return Temp;
            }
            catch
            {
                return Temp;
            }
        }

        private static T getObject<T>(DataRow row, List<string> columnsName) where T : new()
        {
            T obj = new T();
            try
            {
                string columnname = "";
                string value = "";
                PropertyInfo[] Properties;
                Properties = typeof(T).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        value = row[columnname].ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                value = row[columnname].ToString().Replace("%", "");
                                objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }

        public static bool IsNumber(this string valStr)
        {
            string pattern = @"[\D]";
            Regex myRegex = new Regex(pattern);
            if (myRegex.IsMatch(valStr))
            {
                return false;
            }
            return true;
        }



        public static bool IsNotNullOrEmpty(this string str)
        {
            if (str != null)
            {
                return str.Length > 0;
            }
            else
            {
                return false;
            }
        }
        public static int ParseInt32(this string num)
        {
            if (num.IsNumber() && num.IsNotNullOrEmpty())
            {
                return int.Parse(num);
            }
            else
            {
                return 0;
            }
        }

        public static int ParseBool(this bool num)
        {
            if (num==true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public static double ParseDouble(this string num)
        {
            if (num.IsNumber() && num.IsNotNullOrEmpty())
            {
                return double.Parse(num);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// <para>Convert Object to DateTime</para>
        /// <para></para>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime? ParseDateTime(this object date)
        {
            if (date is DateTime)
            {
                return (DateTime)date;
            }
            else
            {
                return null;
            }
        }
    }
}
