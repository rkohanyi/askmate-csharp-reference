using Microsoft.AspNetCore.Http;

namespace AskMateWebApp.Models
{
    public class AddAnswerModel
    {
        public string Message { get; set; }
        public IFormFile Image { get; set; }
    }
}
