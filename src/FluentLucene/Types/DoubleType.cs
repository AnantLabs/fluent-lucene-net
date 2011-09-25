using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="double"/>s.
    /// </summary>
    internal class DoubleType : FieldType<double>
    {
        protected override double GetValueInternal(Field field)
        {
            return double.Parse(field.StringValue());
        }

        protected override void SetValueInternal(Field field, double value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }

        public override bool IsLexicographicallyOrdered { get { return false; } }
    }
}