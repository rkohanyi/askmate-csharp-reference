using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMateWebApp.Services
{
    public interface IQuestionsService
    {
        List<Question> GetAll();

        Question GetOne(int id);
    }
}
