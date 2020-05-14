using System;

namespace AskMate.Common.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationTime { get; set; }
        public long QuestionCount { get; set; }
        public long AnswerCount { get; set; }
        public long CommentCount { get; set; }
    }
}
