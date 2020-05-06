using System;

namespace AskMateWebApp.Domain
{
    public class AskMateCannotVoteException : AskMateException
    {
        public AskMateCannotVoteException(Exception innerException) : base("Cannot vote on owned entity", innerException) { }
    }
}
