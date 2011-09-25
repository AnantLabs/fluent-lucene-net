using System.Globalization;

namespace FluentLucene.Types
{
    /// <summary>
    /// Helper class to convert numbers and other types into a lexicographical representation that
    /// respects natural ordering (essential for range queries and sorting.)
    /// </summary>
    internal class ConvertLex
    {
        // TODO : Convert numbers lexicographically using configurable Precision and Scale (much like databases' DECIMALs)
        
        // Resources:
        // Lucene Range Queries http://wiki.apache.org/lucene-java/SearchNumericalFields
        // MySQL DECIMAL http://dev.mysql.com/doc/refman/5.6/en/precision-math-decimal-changes.html

        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        private const char NegativePrefix = '0';
        private const char PositivePrefix = '1';

        private const int Digits8 = 3;              // 255
        private const int Digits16 = 5;             // 32,767
        private const int Digits32 = 10;            // 2,147,483,647
        private const int Digits64 = 19;            // 9,223,372,036,854,775,807
        private const int DigitsUnsigned64 = 20;    // 18,446,744,073,709,551,615

        public string Int32(int value)
        {
            var negative = value < 0;
            int normalized = value;

            // Invert the magnitude of negative numbers
            if (negative)
            {
                // value is at most -1, so "+ 1" won't ever overflow
                normalized = int.MaxValue + value + 1;
            }

            // Pad the value with zero
            var sValue = normalized.ToString().PadLeft(Digits32, '0');

            // Add a prefix to distinguish positive from negative numbers
            if (negative)
            {
                sValue = NegativePrefix + sValue;
            }
            else
            {
                sValue = PositivePrefix + sValue;
            }

            return sValue;
        }
    }
}