using AskMate.Common.Domain;
using System.Collections.Generic;

namespace AskMate.Web.Models
{
    public class QuestionListModel
    {
        public Question.SortField SortField { get; set; } = Question.SortField.SubmissionTime;
        public bool Ascending { get; set; } = false;
        public List<QuestionListItemModel> Questions { get; set; } = new List<QuestionListItemModel>();
    }
}
