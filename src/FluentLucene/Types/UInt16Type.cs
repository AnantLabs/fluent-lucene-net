using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="ushort"/>s.
    /// </summary>
    internal class UInt16Type : FieldType<ushort>
    {
        protected override ushort GetValueInternal(Field field)
        {
            return ushort.Parse(field.StringValue(), CultureInfo.InvariantCulture);
        }

        protected override void SetValueInternal(Field field, ushort value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }

        public override bool IsLexicographicallyOrdered { get { return false; } }
    }
}