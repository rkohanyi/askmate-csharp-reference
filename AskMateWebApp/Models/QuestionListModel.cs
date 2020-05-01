using AskMateWebApp.Domain;
using System.Collections.Generic;

namespace AskMateWebApp.Models
{
    public class QuestionListModel
    {
        public Question.SortField SortField { get; set; } = Question.SortField.SubmissionTime;
        public bool Ascending { get; set; } = false;
        public List<QuestionListItemModel> Questions { get; set; } = new List<QuestionListItemModel>();
    }
}
