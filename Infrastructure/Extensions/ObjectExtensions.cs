using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        //public static Object GetPropValue(this Object property, string propertyName)
        //{
        //    foreach (var p in propertyName.Split('.'))
        //    {
        //        if (property == null) { return null; }

        //        Type type = property.GetType();
        //        PropertyInfo info = type.GetProperty(part);
        //        if (info == null) { return null; }

        //        property = info.GetValue(property, null);
        //    }
        //    return property;
        //}

        //public static T GetPropValue<T>(this Object obj, String name)
        //{
        //    Object retval = GetPropValue(obj, name);
        //    if (retval == null) { return default(T); }

        //    // throws InvalidCastException if types are incompatible
        //    return (T)retval;
        //}
    }
}
