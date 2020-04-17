using AskMateWebApp.Domain;
using System.Collections.Generic;

namespace AskMateWebApp.Services
{
    public interface IQuestionsService
    {
        List<Question> GetAll();
        Question GetOne(int id);
        int Add(string title, string message);
        void View(int id);
        void Vote(int id, int votes);
        void Delete(int id);
    }
}
