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
        private readonly string _uploadsDirectory;

        public CsvQuestionsService(string csvPath, string uploadsDirectory) : base(csvPath)
        {
            _uploadsDirectory = uploadsDirectory;
        }

        public int Add(string title, string message, string imageFileName, Stream imageStream)
        {
            var questions = GetAll();
            int nextId = questions.Count == 0 ? 1 : questions.Select(x => x.Id).Max() + 1;
            string image = "";
            if (imageFileName != null || imageStream != null)
            {
                image = imageFileName;
                using Stream outputStream = new FileStream(Path.Combine(_uploadsDirectory, imageFileName), FileMode.Create, FileAccess.Write);
                imageStream.CopyTo(outputStream);
            }
            appendTo(nextId, DateTimeOffset.Now.ToUnixTimeSeconds(), 0, 0, image, title, message);
            return nextId;
        }

        public void Update(int id, string title, string message, string imageFileName, Stream imageStream)
        {
            Question q = toQuestion(readFrom(id));
            string image = q.Image;
            if (imageFileName != null || imageStream != null)
            {
                image = imageFileName;
                using Stream outputStream = new FileStream(Path.Combine(_uploadsDirectory, imageFileName), FileMode.Create, FileAccess.Write);
                imageStream.CopyTo(outputStream);
            }
            updateAt(id, q.Id, new DateTimeOffset(q.SubmissionTime).ToUnixTimeSeconds(), q.ViewNumber, q.VoteNumber, image, title, message);
        }

        public List<Question> GetAll(Question.SortField sort, bool ascending)
        {
            var all = readAllFrom()
                .Select(toQuestion);

            if (ascending)
            {
                all = all.OrderBy(x => Question.GetSortField(x, sort));
            }
            else
            {
                all = all.OrderByDescending(x => Question.GetSortField(x, sort));
            }

            return all.ToList();
        }

        public List<Question> GetAll(Question.SortField sort)
        {
            return GetAll(sort, false);
        }

        public List<Question> GetAll(bool ascending)
        {
            return GetAll(Question.SortField.SubmissionTime, ascending);
        }

        public List<Question> GetAll()
        {
            return GetAll(Question.SortField.SubmissionTime, false);
        }

        public Question GetOne(int id)
        {
            return toQuestion(readFrom(id));
        }

        public void View(int id)
        {
            Question q = toQuestion(readFrom(id));
            updateAt(id, q.Id, new DateTimeOffset(q.SubmissionTime).ToUnixTimeSeconds(), q.ViewNumber + 1, q.VoteNumber, q.Image, q.Title, q.Message);
        }

        public void Vote(int id, int votes)
        {
            Question q = toQuestion(readFrom(id));
            updateAt(id, q.Id, new DateTimeOffset(q.SubmissionTime).ToUnixTimeSeconds(), q.ViewNumber, q.VoteNumber + votes, q.Image, q.Title, q.Message);
        }

        public void Delete(int id)
        {
            deleteAt(id);
        }

        private Question toQuestion(string[] fields)
        {
            return new Question
            {
                Id = int.Parse(fields[0]),
                SubmissionTime = DateTimeOffset.FromUnixTimeSeconds(int.Parse(fields[1])).LocalDateTime,
                ViewNumber = int.Parse(fields[2]),
                VoteNumber = int.Parse(fields[3]),
                Image = fields[4],
                Title = fields[5],
                Message = fields[6]
            };
        }
    }
}
