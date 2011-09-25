using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="string"/>s.
    /// </summary>
    internal class StringType : FieldType<string>
    {
        protected override string GetValueInternal(Field field)
        {
            return field.StringValue();
        }

        protected override void SetValueInternal(Field field, string value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}