using AskMateWebApp.Domain;
using System;

namespace AskMateWebApp.Models
{
    public class UserListItemModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime RegistrationTime { get; set; }
        public long QuestionCount { get; set; }
        public long AnswerCount { get; set; }
        public long CommentCount { get; set; }

        public UserListItemModel(User user)
        {
            Id = user.Id;
            Username = user.Username;
            RegistrationTime = user.RegistrationTime;
            QuestionCount = user.QuestionCount;
            AnswerCount = user.AnswerCount;
            CommentCount = user.CommentCount;
        }
    }
}
