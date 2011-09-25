using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="bool"/>s.
    /// </summary>
    internal class BooleanType : FieldType<bool>
    {
        protected override bool GetValueInternal(Field field)
        {
            return bool.Parse(field.StringValue());
        }

        protected override void SetValueInternal(Field field, bool value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }

        public override bool IsLexicographicallyOrdered { get { return true; } }
    }
}