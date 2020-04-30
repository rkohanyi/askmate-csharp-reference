using AskMateWebApp.Domain;
using System.Collections.Generic;
using System.IO;

namespace AskMateWebApp.Services
{
    public interface IQuestionsService
    {
        List<Question> GetAll();
        List<Question> GetAll(long limit);
        List<Question> GetAll(bool ascending);
        List<Question> GetAll(bool ascending, long limit);
        List<Question> GetAll(Question.SortField sort);
        List<Question> GetAll(Question.SortField sort, long limit);
        List<Question> GetAll(Question.SortField sort, bool ascending);
        List<Question> GetAll(Question.SortField sort, bool ascending, long limit);
        Question GetOne(int id);
        int Add(string title, string message, string image);
        void Update(int id, string title, string message, string image);
        void View(int id);
        void Vote(int id, int votes);
        void Delete(int id);
    }
}
