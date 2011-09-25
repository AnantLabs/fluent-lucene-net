using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="uint"/>s.
    /// </summary>
    internal class UInt32Type : FieldType<uint>
    {
        protected override uint GetValueInternal(Field field)
        {
            return uint.Parse(field.StringValue());
        }

        protected override void SetValueInternal(Field field, uint value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }

        public override bool IsLexicographicallyOrdered { get { return false; } }
    }
}