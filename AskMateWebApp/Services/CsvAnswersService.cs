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
        public CsvAnswersService(string csvPath, string uploadsDirectory) : base(csvPath, uploadsDirectory)
        {
        }

        public List<Answer> GetAll(int questionId, Answer.SortField sort, bool ascending)
        {
            var all = ReadAllFrom()
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
            return ToAnswer(ReadFrom(id));
        }

        public int Add(int questionId, string message, string imageFileName, Stream imageStream)
        {
            // IMPORTANT: need to read *all* answers to determine the next ID to use.
            var answers = GetAll();
            int nextId = answers.Count == 0 ? 1 : answers.Select(x => x.Id).Max() + 1;
            string image = SaveTo(imageFileName, imageStream) ?? "";
            AppendTo(nextId, questionId, DateTimeOffset.Now.ToUnixTimeSeconds(), 0, image, message);
            return nextId;
        }

        public void Update(int id, string message, string imageFileName, Stream imageStream)
        {
            Answer a = ToAnswer(ReadFrom(id));
            string image = SaveTo(imageFileName, imageStream) ?? a.Image;
            UpdateAt(id, a.Id, a.QuestionId, new DateTimeOffset(a.SubmissionTime).ToUnixTimeSeconds(), a.VoteNumber, image, message);
        }

        private List<Answer> GetAll()
        {
            return ReadAllFrom()
                .Select(ToAnswer)
                .ToList();
        }

        public void Delete(int id)
        {
            DeleteAt(id);
        }

        public void DeleteAll(int questionId)
        {
            DeleteAt(fields => !fields[1].Equals(questionId.ToString()));
        }

        public void Vote(int id, int votes)
        {
            Answer a = ToAnswer(ReadFrom(id));
            UpdateAt(id, a.Id, a.QuestionId, new DateTimeOffset(a.SubmissionTime).ToUnixTimeSeconds(), a.VoteNumber + votes, a.Image, a.Message);
        }

        private Answer ToAnswer(string[] fields)
        {
            return new Answer
            {
                Id = int.Parse(fields[0]),
                QuestionId = int.Parse(fields[1]),
                SubmissionTime = DateTimeOffset.FromUnixTimeSeconds(int.Parse(fields[2])).LocalDateTime,
                VoteNumber = int.Parse(fields[3]),
                Image = fields[4],
                Message = fields[5]
            };
        }
    }
}
