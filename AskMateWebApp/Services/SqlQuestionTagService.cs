using System.Data;
using System.Linq;

namespace AskMateWebApp.Services
{
    public class SqlQuestionsTagsService : IQuestionsTagsService
    {
        private readonly IDbConnection _connection;

        public SqlQuestionsTagsService(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Add(int questionId, params int[] tagIds)
        {
            if (tagIds.Length == 0)
            {
                return;
            }

            using var command = _connection.CreateCommand();
            command.CommandText = $@"
                INSERT INTO
                    question_tag (question_id, tag_id)
                VALUES
                    {string.Join(", ", tagIds.Select((_, i) => $"(@questionId{i}, @tagId{i})"))}
            ";

            for (int i = 0; i < tagIds.Length; i++)
            {
                var questionIdParam = command.CreateParameter();
                questionIdParam.ParameterName = $"questionId{i}";
                questionIdParam.Value = questionId;
                command.Parameters.Add(questionIdParam);

                var tagIdParam = command.CreateParameter();
                tagIdParam.ParameterName = $"tagId{i}";
                tagIdParam.Value = tagIds[i];
                command.Parameters.Add(tagIdParam);
            }

            command.ExecuteNonQuery();
        }

        public void Delete(int questionId, params int[] tagIds)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = $@"
                DELETE FROM
                    question_tag
                WHERE
                    question_id = @questionId AND
                    tag_id IN ({string.Join(", ", tagIds.Select((_, i) => $"@tagId{i}"))})
            ";

            var questionIdParam = command.CreateParameter();
            questionIdParam.ParameterName = "questionId";
            questionIdParam.Value = questionId;
            command.Parameters.Add(questionIdParam);

            for (int i = 0; i < tagIds.Length; i++)
            {
                var tagIdParam = command.CreateParameter();
                tagIdParam.ParameterName = $"tagId{i}";
                tagIdParam.Value = tagIds[i];
                command.Parameters.Add(tagIdParam);
            }

            command.ExecuteNonQuery();
        }

        public void DeleteAll(int questionId)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "DELETE FROM question_tag WHERE question_id = @questionId";

            var questionIdParam = command.CreateParameter();
            questionIdParam.ParameterName = "questionId";
            questionIdParam.Value = questionId;
            command.Parameters.Add(questionIdParam);

            command.ExecuteNonQuery();
        }
    }
}
