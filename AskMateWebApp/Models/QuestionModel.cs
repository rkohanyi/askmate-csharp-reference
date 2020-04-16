using AskMateWebApp.Domain;

namespace AskMateWebApp.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public QuestionModel(Question question)
        {
            Id = question.Id;
            Title = question.Title;
            Message = question.Message;
        }
    }
}
