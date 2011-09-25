using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="short"/>s.
    /// </summary>
    internal class Int16Type : FieldType<short>
    {
        protected override short GetValueInternal(Field field)
        {
            return short.Parse(field.StringValue());
        }

        protected override void SetValueInternal(Field field, short value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}