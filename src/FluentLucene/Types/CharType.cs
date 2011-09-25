using System.Globalization;
using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="char"/>s.
    /// </summary>
    internal class CharType : FieldType<char>
    {
        protected override char GetValueInternal(Field field)
        {
            return char.Parse(field.StringValue());
        }

        protected override void SetValueInternal(Field field, char value)
        {
            field.SetValue(value.ToString(CultureInfo.InvariantCulture));
        }

        public override bool IsLexicographicallyOrdered { get { return true; } }
    }
}