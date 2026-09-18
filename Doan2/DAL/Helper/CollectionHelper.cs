using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace DAL.Helper
{
    public static class CollectionHelper
    {
        public static List<T> ConvertTo<T>(this DataTable table) where T : new()
        {
            List<T> list = new List<T>();

            if (table == null)
                return list;

            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (PropertyInfo pi in typeof(T).GetProperties())
            {
                if (table.Columns.Contains(pi.Name))
                {
                    properties.Add(pi);
                }
            }

            foreach (DataRow row in table.Rows)
            {
                T item = new T();
                foreach (PropertyInfo pi in properties)
                {
                    if (row[pi.Name] != DBNull.Value)
                    {
                        Type targetType = Nullable.GetUnderlyingType(pi.PropertyType) ?? pi.PropertyType;
                        object value = Convert.ChangeType(row[pi.Name], targetType);
                        pi.SetValue(item, value, null);
                    }
                }
                list.Add(item);
            }

            return list;
        }
    }
}