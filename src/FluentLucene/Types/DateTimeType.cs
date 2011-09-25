using System;
using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="DateTime"/>s.
    /// </summary>
    internal class DateTimeType : FieldType<DateTime>
    {
        protected override DateTime GetValueInternal(Field field)
        {
            return DateTime.ParseExact(field.StringValue(), "o", CultureInfo.InvariantCulture);
        }

        protected override void SetValueInternal(Field field, DateTime value)
        {
            field.SetValue(value.ToString("o", CultureInfo.InvariantCulture));
        }

        public override bool IsLexicographicallyOrdered { get { return true; } }
    }
}