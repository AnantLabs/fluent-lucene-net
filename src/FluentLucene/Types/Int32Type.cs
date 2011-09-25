using Lucene.Net.Documents;

namespace FluentLucene.Types
{
    /// <summary>
    /// Mapping for <see cref="int"/>s.
    /// </summary>
    internal class Int32Type : NumericFieldType<int>
    {
        protected override int GetValueInternal(NumericField field)
        {
            return (int)field.GetNumericValue();
        }

        protected override void SetValueInternal(NumericField field, int value)
        {
            field.SetIntValue(value);
        }
    }
}