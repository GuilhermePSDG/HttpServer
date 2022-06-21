using System.Runtime.Serialization;

namespace ServidorHttp.Fluent
{
    [Serializable]
    internal class DuplicatedResolverPriorityException : Exception
    {
        public DuplicatedResolverPriorityException()
        {
        }

        public DuplicatedResolverPriorityException(string? message) : base(message)
        {
        }

        public DuplicatedResolverPriorityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DuplicatedResolverPriorityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}