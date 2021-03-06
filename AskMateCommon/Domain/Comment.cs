using System;

namespace AskMate.Common.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int EditedNumber { get; set; }
        public string Message { get; set; }
    }
}
