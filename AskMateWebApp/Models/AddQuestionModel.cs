using Microsoft.AspNetCore.Http;

namespace AskMateWebApp.Models
{
    public class AddQuestionModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public IFormFile Image { get; set; }
    }
}
