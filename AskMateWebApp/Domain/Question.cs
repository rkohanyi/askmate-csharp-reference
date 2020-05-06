using System;

namespace AskMateWebApp.Domain
{
    public class Question
    {
        public static object GetSortField(Question q, SortField sort)
        {
            switch (sort)
            {
                case SortField.Title: return q.Title;
                case SortField.ViewNumber: return q.ViewNumber;
                case SortField.VoteNumber: return q.VoteNumber;
                case SortField.SubmissionTime: return q.SubmissionTime;
                default:
                    throw new InvalidOperationException();
            }
        }

        public enum SortField
        {
            SubmissionTime,
            Title,
            ViewNumber,
            VoteNumber
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int ViewNumber { get; set; }
        public int VoteNumber { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
    }
}
