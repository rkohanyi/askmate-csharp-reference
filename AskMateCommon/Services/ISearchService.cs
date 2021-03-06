using AskMate.Common.Domain;
using System.Collections.Generic;

namespace AskMate.Common.Services
{
    public interface ISearchService
    {
        public string StartDelimiter { get; }
        public string StopDelimiter { get; }

        Dictionary<Question, List<Answer>> SearchAll(string phrase);
        Dictionary<Question, List<Answer>> SearchAll(string phrase, long limit);
    }
}
