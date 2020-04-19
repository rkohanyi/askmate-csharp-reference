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

        public List<Answer> GetAll(int questionId, Answer.SortField sort, bool ascending)
        {
            var all = readAllFrom()
                .Select(ToAnswer)
                .Where(x => x.QuestionId == questionId);

            if (ascending)
            {
                all = all.OrderBy(x => Answer.GetSortField(x, sort));
            }
            else
            {
                all = all.OrderByDescending(x => Answer.GetSortField(x, sort));
            }

            return all.ToList();
        }

        public List<Answer> GetAll(int questionId, Answer.SortField sort)
        {
            return GetAll(questionId, sort, false);
        }

        public List<Answer> GetAll(int questionId, bool ascending)
        {
            return GetAll(questionId, Answer.SortField.SubmissionTime, ascending);
        }

        public List<Answer> GetAll(int questionId)
        {
            return GetAll(questionId, Answer.SortField.SubmissionTime, false);
        }

        public Answer GetOne(int id)
        {
            return ToAnswer(readFrom(id));
        }

        public int Add(int questionId, string message)
        {
            // IMPORTANT: need to read *all* answers to determine the next ID to use.
            var answers = GetAll();
            int nextId = answers.Count == 0 ? 1 : answers.Select(x => x.Id).Max() + 1;
            appendTo(nextId, questionId, DateTimeOffset.Now.ToUnixTimeSeconds(), 0, message);
            return nextId;
        }

        private List<Answer> GetAll()
        {
            return readAllFrom()
                .Select(ToAnswer)
                .ToList();
        }

        public void Delete(int id)
        {
            deleteAt(id);
        }

        public void DeleteAll(int questionId)
        {
            deleteAt(fields => !fields[1].Equals(questionId.ToString()));
        }

        public void Vote(int id, int votes)
        {
            Answer a = ToAnswer(readFrom(id));
            updateAt(id, a.Id, a.QuestionId, new DateTimeOffset(a.SubmissionTime).ToUnixTimeSeconds(), a.VoteNumber + votes, a.Message);
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
