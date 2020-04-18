using System;

namespace AskMateWebApp.Domain
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int VoteNumber { get; set; }
        public string Message { get; set; }
    }
}
