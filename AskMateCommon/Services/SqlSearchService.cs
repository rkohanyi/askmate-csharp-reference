using AskMateCommon.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AskMateCommon.Services
{
    public class SqlSearchService : ISearchService
    {
        private string startDelimiter;
        private string stopDelimiter;

        public string StartDelimiter
        {
            get => startDelimiter;
            private set => startDelimiter = value.Replace("\"", "\"\"");
        }

        public string StopDelimiter
        {
            get => stopDelimiter;
            private set => stopDelimiter = value.Replace("\"", "\"\"");
        }

        private readonly IDbConnection _connection;

        public SqlSearchService(IDbConnection connection, string startDelimiter, string stopDelimiter)
        {
            _connection = connection;
            StartDelimiter = startDelimiter;
            StopDelimiter = stopDelimiter;
        }

        public Dictionary<Question, List<Answer>> SearchAll(string phrase)
        {
            return SearchAll(phrase, 0);
        }

        public Dictionary<Question, List<Answer>> SearchAll(string phrase, long limit)
        {
            using var command = _connection.CreateCommand();
            string sql = @$"
                SELECT
                    q.id AS question_id,
                    q.submission_time AS question_submission_time,
                    q.vote_number AS question_vote_number,
                    q.view_number AS question_view_number,
                    q.image AS question_image,
                    ts_headline('english', q.title, phrase, 'StartSel=""{startDelimiter}"", StopSel=""{stopDelimiter}"", HighlightAll=true, MaxFragments=0') AS question_title,
	                ts_headline('english', q.message, phrase, 'StartSel=""{startDelimiter}"", StopSel=""{stopDelimiter}"", HighlightAll=true, MaxFragments=0') AS question_message,
                    a.id AS answer_id,
                    a.submission_time AS answer_submission_time,
                    a.vote_number AS answer_vote_number,
                    a.image AS answer_image,
                    ts_headline('english', a.message, phrase, 'StartSel=""{startDelimiter}"", StopSel=""{stopDelimiter}"", HighlightAll=true, MaxFragments=0') AS answer_message
                FROM
                    question AS q
                CROSS JOIN
                    websearch_to_tsquery('english', @phrase) AS phrase
                LEFT JOIN
                    answer AS a ON q.id = a.question_id AND round(ts_rank(a.document, phrase)::numeric, 8) > 0
                WHERE
                    q.document @@ phrase OR a.document @@ phrase
                ORDER BY
                    round(ts_rank(q.document, phrase)::numeric, 8) DESC,
                    round(ts_rank(a.document, phrase)::numeric, 8) DESC,
					q.submission_time DESC,
					a.submission_time DESC
            ";
            if (limit > 0)
            {
                sql += $" LIMIT {limit}";
            }
            command.CommandText = sql;

            var phraseParam = command.CreateParameter();
            phraseParam.ParameterName = "phrase";
            phraseParam.Value = phrase;

            command.Parameters.Add(phraseParam);

            using var reader = command.ExecuteReader();
            Dictionary<int, Question> questions = new Dictionary<int, Question>();
            Dictionary<int, List<Answer>> answers = new Dictionary<int, List<Answer>>();
            while (reader.Read())
            {
                int questionId = (int)reader["question_id"];
                if (!questions.ContainsKey(questionId))
                {
                    questions.Add(questionId, new Question
                    {
                        Id = questionId,
                        SubmissionTime = (DateTime)reader["question_submission_time"],
                        Title = reader["question_title"] as string,
                        Message = reader["question_message"] as string,
                        ViewNumber = (int)reader["question_view_number"],
                        VoteNumber = (int)reader["question_vote_number"],
                        Image = reader["question_image"] as string,
                    });
                }
                if (!answers.ContainsKey(questionId))
                {
                    answers[questionId] = new List<Answer>();
                }
                if (reader["answer_id"] is int answerId)
                {
                    answers[questionId].Add(new Answer
                    {
                        Id = answerId,
                        QuestionId = questionId,
                        SubmissionTime = (DateTime)reader["answer_submission_time"],
                        VoteNumber = (int)reader["answer_vote_number"],
                        Message = reader["answer_message"] as string,
                        Image = reader["answer_image"] as string,
                    });
                }
            }
            return questions.Values.ToDictionary(q => q, q => answers[q.Id]);
        }
    }
}
