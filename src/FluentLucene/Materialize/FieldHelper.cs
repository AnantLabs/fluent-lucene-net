using System;
using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Materialize
{
    public static class FieldHelper
    {
        public static int GetInt32(this Field field)
        {
            return int.Parse(field.StringValue(), CultureInfo.InvariantCulture);
        }

        public static object GetEnum(this Field field, Type enumType)
        {
            return Enum.Parse(enumType, field.StringValue());
        }

        public static decimal GetDecimal(this Field field)
        {
            return decimal.Parse(field.StringValue(), CultureInfo.InvariantCulture);
        }

        public static DateTime GetDateTime(this Field field)
        {
            return DateTime.Parse(field.StringValue(), CultureInfo.InvariantCulture);
        }

        public static double GetDouble(this Field field)
        {
            return double.Parse(field.StringValue(), CultureInfo.InvariantCulture);
        }

        public static bool GetBoolean(this Field field)
        {
            return bool.Parse(field.StringValue());
        }
    }
}