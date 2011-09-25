using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="byte"/>s.
    /// </summary>
    internal class ByteType : FieldType<byte>
    {
        protected override byte GetValueInternal(Field field)
        {
            return byte.Parse(field.StringValue());
        }

        protected override void SetValueInternal(Field field, byte value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }
    }
}