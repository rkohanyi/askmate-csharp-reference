using AskMateCommon.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public string Image { get; set; }
        public List<TagModel> Tags { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<AnswerModel> Answers { get; set; }

        public QuestionDetailModel(
            Question question,
            List<Tag> tags,
            List<Comment> comments,
            List<Answer> answers,
            Dictionary<int, List<Comment>> answerComments)
        {
            Id = question.Id;
            SubmissionTime = question.SubmissionTime;
            ViewNumber = question.ViewNumber;
            VoteNumber = question.VoteNumber;
            Title = question.Title;
            Message = question.Message;
            Image = question.Image;
            Tags = tags.Select(x => new TagModel(x)).ToList();
            Comments = comments.Select(x => new CommentModel(x)).ToList();
            Answers = answers.Select(x => new AnswerModel(x, answerComments[x.Id])).ToList();
        }
    }
}
