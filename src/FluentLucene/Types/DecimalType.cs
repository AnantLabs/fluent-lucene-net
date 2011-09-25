using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="decimal"/>s.
    /// </summary>
    internal class DecimalType : FieldType<decimal>
    {
        protected override decimal GetValueInternal(Field field)
        {
            return decimal.Parse(field.StringValue());
        }

        protected override void SetValueInternal(Field field, decimal value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }

        public override bool IsLexicographicallyOrdered { get { return false; } }
    }
}