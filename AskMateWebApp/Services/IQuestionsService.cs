using AskMateWebApp.Domain;
using System.Collections.Generic;

namespace AskMateWebApp.Services
{
    public interface IQuestionsService
    {
        List<Question> GetAll();
        List<Question> GetAll(bool ascending);
        List<Question> GetAll(Question.SortField sort);
        List<Question> GetAll(Question.SortField sort, bool ascending);
        Question GetOne(int id);
        int Add(string title, string message);
        void Update(int id, string title, string message);
        void View(int id);
        void Vote(int id, int votes);
        void Delete(int id);
    }
}
