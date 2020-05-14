using Microsoft.AspNetCore.Http;

namespace AskMate.Web.Models
{
    public class AddQuestionModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public IFormFile Image { get; set; }
    }
}
