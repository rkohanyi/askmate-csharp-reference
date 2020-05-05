using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AskMateWebApp.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int VoteNumber { get; set; }
        public string Image { get; set; }
        public string Message { get; set; }
        public List<CommentModel> Comments { get; set; }

        public AnswerModel(Answer answer) : this(answer, new List<Comment>()) { }

        public AnswerModel(Answer answer, List<Comment> comments)
        {
            Id = answer.Id;
            QuestionId = answer.QuestionId;
            SubmissionTime = answer.SubmissionTime;
            VoteNumber = answer.VoteNumber;
            Image = answer.Image;
            Message = answer.Message;
            Comments = comments.Select(x => new CommentModel(x)).ToList();
        }
    }
}
