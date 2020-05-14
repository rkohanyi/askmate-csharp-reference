using System;

namespace AskMateCommon.Domain
{
    public class AskMateNotAuthorizedException : AskMateException
    {
        public AskMateNotAuthorizedException(Exception innerException) : base("Not authorized", innerException) { }
    }
}
