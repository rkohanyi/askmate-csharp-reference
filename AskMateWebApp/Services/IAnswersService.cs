using AskMateWebApp.Domain;
using System.Collections.Generic;

namespace AskMateWebApp.Services
{
    public interface IAnswersService
    {
        List<Answer> GetAll(int questionId);
        Answer GetOne(int id);
        int Add(int questionId, string message);
        void Delete(int id);
        void DeleteAll(int questionId);
        void Vote(int id, int votes);
    }
}
