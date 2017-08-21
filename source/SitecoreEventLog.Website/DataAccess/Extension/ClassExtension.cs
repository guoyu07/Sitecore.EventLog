using System;
using System.Reflection;

namespace SitecoreEventLog.Website.DataAccess.Extension
{
    public static class ClassExtension
    {
        public static void SetProperty(this object item, PropertyInfo propertyinfo, object value)
        {
            if (value is decimal)
                propertyinfo.SetValue(item, Convert.ToDouble(value), null);
            else
                propertyinfo.SetValue(item, value, null);
        }

        public static T Default<T>()
        {
            if (!typeof(T).IsValueType && typeof(T) != typeof(string))
                return Activator.CreateInstance<T>();

            return default(T);
        }
    }
}