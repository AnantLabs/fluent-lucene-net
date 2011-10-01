using System;
using System.Runtime.Serialization;

namespace FluentLucene
{
    /// <summary>
    /// Base exception for every exception thrown by FluentLucene
    /// </summary>
    [Serializable]
    public class FluentLuceneException : Exception
    {
        public FluentLuceneException()
        { }

        public FluentLuceneException(string message)
            : base(message)
        { }

        public FluentLuceneException(string message, Exception inner)
            : base(message, inner)
        { }

        protected FluentLuceneException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}