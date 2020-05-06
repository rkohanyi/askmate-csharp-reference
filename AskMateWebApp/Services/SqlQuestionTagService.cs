using AskMateWebApp.Domain;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace AskMateWebApp.Services
{
    public class SqlQuestionsTagsService : IQuestionsTagsService
    {
        private readonly IDbConnection _connection;

        public SqlQuestionsTagsService(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Add(int userId, int questionId, params int[] tagIds)
        {
            if (tagIds.Length == 0)
            {
                return;
            }

            using var command = _connection.CreateCommand();
            command.CommandText = @$"SELECT add_question_tag (
                    @userId,
                    @questionId, 
                    ARRAY[{string.Join(", ", tagIds.Select((_, i) => $"@tagId{i}"))}]
            )";

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userId";
            userIdParam.Value = userId;
            command.Parameters.Add(userIdParam);

            var questionIdParam = command.CreateParameter();
            questionIdParam.ParameterName = "questionId";
            questionIdParam.Value = questionId;
            command.Parameters.Add(questionIdParam);

            for (int i = 0; i < tagIds.Length; i++)
            {
                var tagIdParam = command.CreateParameter();
                tagIdParam.DbType = DbType.Int32;
                tagIdParam.ParameterName = $"tagId{i}";
                tagIdParam.Value = tagIds[i];
                command.Parameters.Add(tagIdParam);
            }

            try
            {
                command.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                string sqlState = (string)ex.Data["SqlState"];
                if (sqlState == "45000")
                {
                    throw new AskMateNotAuthorizedException(ex);
                }
                throw;
            }
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
