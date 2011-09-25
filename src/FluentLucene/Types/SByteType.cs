using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="sbyte"/>s.
    /// </summary>
    internal class SByteType : FieldType<sbyte>
    {
        protected override sbyte GetValueInternal(Field field)
        {
            return sbyte.Parse(field.StringValue());
        }

        protected override void SetValueInternal(Field field, sbyte value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}