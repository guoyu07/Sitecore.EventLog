using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SitecoreEventLog.Website.DataAccess.Extension
{
    public static class DataTableExtension
    {
        public static List<T> ToClassList<T>(this DataTable data)
        {
            var properties = typeof(T).GetProperties();

            var result = new List<T>();
            foreach (DataRow dataRow in data.Rows)
                result.Add(dataRow.ToClass<T>(properties));

            return result;
        }

        private static T ToClass<T>(this DataRow dataRow, IEnumerable<PropertyInfo> properties)
        {
            var result = ClassExtension.Default<T>();

            foreach (var property in properties)
                if (dataRow.Table.Columns.Contains(property.Name) && !(dataRow[property.Name] is DBNull))
                    result.SetProperty(property, dataRow[property.Name]);

            return result;
        }
    }
}