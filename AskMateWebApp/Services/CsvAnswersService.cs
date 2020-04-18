using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            var answers = GetAll();
            int nextId = answers.Count == 0 ? 1 : answers.Select(x => x.Id).Max() + 1;
            appendTo(nextId, questionId, DateTimeOffset.Now.ToUnixTimeSeconds(), 0, message);
            return nextId;
        }

        public void Delete(int id)
        {
            deleteAt(id);
        }

        public void DeleteAll(int questionId)
        {
            deleteAt(fields => !fields[1].Equals(questionId.ToString()));
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
                SubmissionTime = DateTimeOffset.FromUnixTimeSeconds(int.Parse(fields[2])).LocalDateTime,
                VoteNumber = int.Parse(fields[3]),
                Message = fields[4]
            };
        }
    }
}
