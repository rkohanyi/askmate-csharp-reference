using AskMateWebApp.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AskMateWebApp.Services
{
    public class InMemoryQuestionsService : IQuestionsService
    {
        private readonly List<Question> _questions = new List<Question>();

        public InMemoryQuestionsService()
        {
            _questions.Add(new Question { Id = 1, Title = "How much is the fish?", Message = "I want to know, how much it is?" });
            _questions.Add(new Question { Id = 2, Title = "How far is the Moon?", Message = "I wonder, how far it is?" });
            _questions.Add(new Question { Id = 3, Title = "How old is the bus driver?", Message = "Do you how old he is?" });
        }

        public List<Question> GetAll()
        {
            return _questions;
        }

        public Question GetOne(int id)
        {
            foreach (var q in _questions)
            {
                if (q.Id == id)
                {
                    return q;
                }
            }
            return null;
        }

        public int Add(string title, string message)
        {
            int nextId = _questions.Select(x => x.Id).Max() + 1;
            _questions.Add(new Question { Id = nextId, Title = title, Message = message });
            return nextId;
        }
    }
}
