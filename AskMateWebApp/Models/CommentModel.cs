using AskMateWebApp.Domain;
using System;

namespace AskMateWebApp.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int EditedNumber { get; set; }
        public string Message { get; set; }

        public CommentModel(Comment comment)
        {
            Id = comment.Id;
            QuestionId = comment.QuestionId;
            AnswerId = comment.AnswerId;
            SubmissionTime = comment.SubmissionTime;
            EditedNumber = comment.EditedNumber;
            Message = comment.Message;
        }
    }
}
