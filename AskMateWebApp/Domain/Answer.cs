using System;

namespace AskMateWebApp.Domain
{
    public class Answer
    {
        public static object GetSortField(Answer a, SortField sort)
        {
            switch (sort)
            {
                case SortField.VoteNumber: return a.VoteNumber;
                case SortField.SubmissionTime: return a.SubmissionTime;
                default:
                    throw new InvalidOperationException();
            }
        }

        public enum SortField
        {
            SubmissionTime,
            VoteNumber
        }

        public int Id { get; set; }
        public int QuestionId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int VoteNumber { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
    }
}
