using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentLucene.Tests.TestUtils
{
    /// <summary>
    /// General helper class for getting supposedly supported types
    /// </summary>
    public static class SupportedTypes
    {
        /// <summary>
        /// Gets all .NET native types that ought to be supported by FluentLucene. DOES NOT INCLUDE ENUMS.
        /// </summary>
        public static IEnumerable<Type> NativeTypes
        {
            get
            {
                yield return typeof(bool);
                yield return typeof(byte);
                yield return typeof(char);
                yield return typeof(short);
                yield return typeof(int);
                yield return typeof(long);
                yield return typeof(ushort);
                yield return typeof(uint);
                yield return typeof(ulong);
                yield return typeof(sbyte);
                yield return typeof(decimal);
                yield return typeof(float);
                yield return typeof(double);
                yield return typeof(string);
            }
        }

        /// <summary>
        /// Gets all .NET native types that are value types, returned as their Nullable version (int?, long?, etc.)
        /// </summary>
        public static IEnumerable<Type> NullableNativeTypes
        {
            get { return NativeTypes.Where(x => x.IsValueType).Select(x => typeof(Nullable<>).MakeGenericType(x)); }
        }

        /// <summary>
        /// Get all complementary .NET types that ought to be supported by FluentLucene
        /// </summary>
        public static IEnumerable<Type> ComplementaryType
        {
            get
            {
                yield return typeof(DateTime);
                yield return typeof(TimeSpan);
            }
        }

        /// <summary>
        /// Gets all complementary .NET types that are value types, returned as their Nullable version (DateTime?, etc.)
        /// </summary>
        public static IEnumerable<Type> NullableComplementaryType
        {
            get { return ComplementaryType.Where(x => x.IsValueType).Select(x => typeof(Nullable<>).MakeGenericType(x)); }
        }

        /// <summary>
        /// Gets all supported types. DOES NOT INCLUDE ENUMS.
        /// </summary>
        public static IEnumerable<Type> All
        {
            get
            {
                return new []
                {
                    NativeTypes, 
                    NullableNativeTypes, 
                    ComplementaryType, 
                    NullableComplementaryType

                }.SelectMany(x => x);
            }
        }
    }
}