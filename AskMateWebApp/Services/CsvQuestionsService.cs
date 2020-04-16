using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AskMateWebApp.Services
{
    public class CsvQuestionsService : BaseCsvService, IQuestionsService
    {
        public CsvQuestionsService(string csvPath) : base(csvPath)
        {
        }

        public int Add(string title, string message)
        {
            int nextId = GetAll().Select(x => x.Id).Max() + 1;
            appendTo(nextId, title, message);
            return nextId;
        }

        public List<Question> GetAll()
        {
            return readAllFrom()
                .Select(toQuestion)
                .ToList();
        }

        public Question GetOne(int id)
        {
            return toQuestion(readFrom(id));
        }

        private Question toQuestion(string[] fields)
        {
            return new Question
            {
                Id = int.Parse(fields[0]),
                Title = fields[1],
                Message = fields[2]
            };
        }
    }
}
