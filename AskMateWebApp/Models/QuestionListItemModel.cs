using AskMate.Common.Domain;
using System;

namespace AskMate.Web.Models
{
    public class QuestionListItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int ViewNumber { get; set; }
        public int VoteNumber { get; set; }

        public QuestionListItemModel(Question question)
        {
            Id = question.Id;
            Title = question.Title;
            Message = question.Message;
            SubmissionTime = question.SubmissionTime;
            ViewNumber = question.ViewNumber;
            VoteNumber = question.VoteNumber;
        }
    }
}
