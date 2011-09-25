using System;
using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="TimeSpan"/>s.
    /// </summary>
    internal class TimeSpanType : FieldType<TimeSpan>
    {
        protected override TimeSpan GetValueInternal(Field field)
        {
            return TimeSpan.ParseExact(field.StringValue(), "c", CultureInfo.InvariantCulture);
        }

        protected override void SetValueInternal(Field field, TimeSpan value)
        {
            field.SetValue(value.ToString("c", CultureInfo.InvariantCulture));
        }
    }
}