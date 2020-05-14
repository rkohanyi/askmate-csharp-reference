using AskMateCommon.Domain;
using Microsoft.AspNetCore.Http;

namespace AskMateWebApp.Models
{
    public class AddAnswerModel
    {
        public int QuestionId { get; set; }
        public string Message { get; set; }
        public IFormFile Image { get; set; }

        public AddAnswerModel() { }

        public AddAnswerModel(Answer answer)
        {
            QuestionId = answer.QuestionId;
            Message = answer.Message;
        }
    }
}
