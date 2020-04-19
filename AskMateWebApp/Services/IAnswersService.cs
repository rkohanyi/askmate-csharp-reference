using AskMateWebApp.Domain;
using System.Collections.Generic;

namespace AskMateWebApp.Services
{
    public interface IAnswersService
    {
        List<Answer> GetAll(int questionId);
        List<Answer> GetAll(int questionId, bool ascending);
        List<Answer> GetAll(int questionId, Answer.SortField sort);
        List<Answer> GetAll(int questionId, Answer.SortField sort, bool ascending);
        Answer GetOne(int id);
        int Add(int questionId, string message);
        void Delete(int id);
        void DeleteAll(int questionId);
        void Vote(int id, int votes);
    }
}
