using AskMateWebApp.Domain;
using System.Collections.Generic;

namespace AskMateWebApp.Services
{
    public interface IAnswersService
    {
        List<Answer> GetAll(int questionId);
        Answer GetOne(int id);
        int Add(int questionId, string message);
    }
}
