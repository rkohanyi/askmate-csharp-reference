using System;

namespace AskMateWebApp.Domain
{
    public class AskMateException : Exception
    {
        public AskMateException() { }
        public AskMateException(string message) : base(message) { }
        public AskMateException(string message, Exception innerException) : base(message, innerException) { }
    }
}
