using System;

namespace FluentLucene.Tests.Mapping
{
    public class SampleDocument
    {
        public event EventHandler Event;

        public int Id { get; set; }
        public int IdPrivateSet { get; private set; }
        public int IdInternalSet { get; protected set; }
        public int IdProtectedSet { get; protected set; }

        public string StringValue { get; set; }
        public string StringComputed { get { return "StringComputed"; } }

        public DateTime DateValue { get; set; }
        public DateTime DateComputed { get { return DateValue.AddDays(1); } }

        public int IntegerValue { get; set; }
        public int IntegerComputed { get { return IntegerValue + 1; } }

        public string FieldPublic;
        public string FieldInternal;

        public string PropertyPublic { get; set; }
        public string PropertyPublicPrivate { get; private set; }
        internal string PropertyInternal { get; set; }
        internal string PropertyInternalPrivate { get; private set; }

        public object MethodCall()
        {
            return null;
        }
    }
}