using AskMateWebApp.Domain;
using Microsoft.AspNetCore.Http;

namespace AskMateWebApp.Models
{
    public class AddCommentModel
    {
        public int QuestionId { get; set; }
        public string Message { get; set; }

        public AddCommentModel() { }

        public AddCommentModel(int questionId, string message)
        {
            QuestionId = questionId;
            Message = message;
        }
    }
}
