using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="int"/>s.
    /// </summary>
    internal class Int32Type : FieldType<int>
    {
        protected override int GetValueInternal(Field field)
        {
            return int.Parse(field.StringValue(), CultureInfo.InvariantCulture);
        }

        protected override void SetValueInternal(Field field, int value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}