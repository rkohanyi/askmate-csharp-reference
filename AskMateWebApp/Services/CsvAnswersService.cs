using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AskMateWebApp.Services
{
    public class CsvAnswersService : BaseCsvService, IAnswersService
    {
        public CsvAnswersService(string csvPath) : base(csvPath)
        {
        }

        public List<Answer> GetAll(int questionId)
        {
            return GetAll()
                .Where(x => x.QuestionId == questionId)
                .ToList();
        }

        public Answer GetOne(int id)
        {
            return ToAnswer(readFrom(id));
        }

        public int Add(int questionId, string message)
        {
            int nextId = GetAll().Select(x => x.Id).Max() + 1;
            appendTo(nextId, questionId, message);
            return nextId;
        }

        private List<Answer> GetAll()
        {
            return readAllFrom()
                .Select(ToAnswer)
                .ToList();
        }

        private Answer ToAnswer(string[] fields)
        {
            return new Answer
            {
                Id = int.Parse(fields[0]),
                QuestionId = int.Parse(fields[1]),
                Message = fields[2]
            };
        }
    }
}
