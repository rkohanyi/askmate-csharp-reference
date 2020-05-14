using AskMate.Common.Domain;
using System.Collections.Generic;

namespace AskMate.Common.Services
{
    public interface IAnswersService
    {
        public sealed class GetAllOptions
        {
            public int? UserId { get; set; }
            public int? QuestionId { get; set; }
            public Answer.SortField Sort { get; set; }
            public bool Ascending { get; set; }

            public GetAllOptions() : this(Answer.SortField.SubmissionTime, false) { }

            public GetAllOptions(Answer.SortField sort, bool ascending)
            {
                Sort = sort;
                Ascending = ascending;
            }
        }

        List<Answer> GetAll(GetAllOptions opts);
        Answer GetOne(int id);
        int Add(int userId, int questionId, string message, string image);
        void Update(int userId, int id, string message, string image);
        void Delete(int userId, int id);
        void DeleteAll(int userId, int questionId);
        void Vote(int userId, int id, int votes);
    }
}
