using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="ulong"/>s.
    /// </summary>
    internal class UInt64Type : FieldType<ulong>
    {
        protected override ulong GetValueInternal(Field field)
        {
            return ulong.Parse(field.StringValue());
        }

        protected override void SetValueInternal(Field field, ulong value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }

        public override bool IsLexicographicallyOrdered { get { return false; } }
    }
}