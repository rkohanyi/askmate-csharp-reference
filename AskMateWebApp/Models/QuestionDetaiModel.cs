using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;

namespace AskMateWebApp.Models
{
    public class QuestionDetailModel
    {
        public int Id { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int ViewNumber { get; set; }
        public int VoteNumber { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public List<Answer> Answers { get; set; }

        public QuestionDetailModel(Question question, List<Answer> answers)
        {
            Id = question.Id;
            SubmissionTime = question.SubmissionTime;
            ViewNumber = question.ViewNumber;
            VoteNumber = question.VoteNumber;
            Title = question.Title;
            Message = question.Message;
            Answers = answers;
        }
    }
}
