using System;

namespace AskMateWebApp.Domain
{
    public class AskMateNotAuthorizedException : AskMateException
    {
        public AskMateNotAuthorizedException(Exception innerException) : base("Not authorized", innerException) { }
    }
}
