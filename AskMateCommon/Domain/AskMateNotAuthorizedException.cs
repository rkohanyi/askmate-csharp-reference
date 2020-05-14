using System;

namespace AskMate.Common.Domain
{
    public class AskMateNotAuthorizedException : AskMateException
    {
        public AskMateNotAuthorizedException(Exception innerException) : base("Not authorized", innerException) { }
    }
}
