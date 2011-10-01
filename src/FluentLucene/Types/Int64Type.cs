using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="long"/>s.
    /// </summary>
    internal class Int64Type : FieldType<long>
    {
        protected override long GetValueInternal(Field field)
        {
            return long.Parse(field.StringValue(), CultureInfo.InvariantCulture);
        }

        protected override void SetValueInternal(Field field, long value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }

        public override bool IsLexicographicallyOrdered { get { return false; } }
    }
}