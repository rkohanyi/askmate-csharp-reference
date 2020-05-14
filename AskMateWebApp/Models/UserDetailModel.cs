using AskMate.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AskMate.Web.Models
{
    public class UserDetailModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationTime { get; set; }
        public List<QuestionListItemModel> Questions { get; set; }
        public List<AnswerModel> Answers { get; set; }
        public List<CommentModel> Comments { get; set; }

        public UserDetailModel(User user, List<Question> questions, List<Answer> answers, List<Comment> comments)
        {
            Id = user.Id;
            Username = user.Username;
            RegistrationTime = user.RegistrationTime;
            Questions = questions.Select(x => new QuestionListItemModel(x)).ToList();
            Answers = answers.Select(x => new AnswerModel(x)).ToList();
            Comments = comments.Select(x => new CommentModel(x)).ToList();
        }
    }
}
