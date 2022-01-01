using System;

namespace Domain
{
    [System.Serializable]
    public class DomainException : Exception
    {
        public DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, System.Exception inner) : base(message, inner) { }
        protected DomainException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}