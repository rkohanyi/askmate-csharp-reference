using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMateWebApp.Services
{
    public class InMemoryQuestionsService : IQuestionsService
    {
        private List<Question> _questions = new List<Question>();

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
    }
}
