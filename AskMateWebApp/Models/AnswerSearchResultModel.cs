using AskMateCommon.Domain;
using System;

namespace AskMateWebApp.Models
{
    public class AnswerSearchResultModel
    {
        public int Id { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int VoteNumber { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }

        public AnswerSearchResultModel(Answer answer)
        {
            Id = answer.Id;
            SubmissionTime = answer.SubmissionTime;
            VoteNumber = answer.VoteNumber;
            Image = answer.Image;
            Message = answer.Message;
        }
    }
}
