using System;
using System.Runtime.Serialization;

namespace Application
{
    [System.Serializable]
    public class UserNotAuthorizedException : Exception
    {
        public UserNotAuthorizedException(string message = "This user cannot modify this board.") : base(message) { }
        public UserNotAuthorizedException(string message, System.Exception inner) : base(message, inner) { }
        protected UserNotAuthorizedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}