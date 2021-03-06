using AskMate.Common.Domain;
using System.Collections.Generic;

namespace AskMate.Common.Services
{
    public interface IQuestionsService
    {
        public sealed class GetAllOptions
        {
            public int? UserId { get; set; }
            public Question.SortField Sort { get; set; }
            public bool Ascending { get; set; }
            public long? Limit { get; set; }

            public GetAllOptions() : this(Question.SortField.SubmissionTime, false) { }

            public GetAllOptions(Question.SortField sort, bool ascending)
            {
                Sort = sort;
                Ascending = ascending;
            }
        }

        List<Question> GetAll(GetAllOptions opts);
        Question GetOne(int id);
        int Add(int userId, string title, string message, string image);
        void Update(int userId, int id, string title, string message, string image);
        void View(int? userId, int id);
        void Vote(int userId, int id, int votes);
        void Delete(int userId, int id);
    }
}
