using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace AskMateWebApp.Services
{
    public class SqlAnswersService : IAnswersService
    {
        private static Answer ToAnswer(IDataReader reader)
        {
            return new Answer
            {
                Id = (int)reader["id"],
                QuestionId = (int)reader["question_id"],
                SubmissionTime = (DateTime)reader["submission_time"],
                Message = reader["message"] as string,
                VoteNumber = (int)reader["vote_number"],
                Image = reader["image"] as string,
            };
        }

        private readonly IDbConnection _connection;

        public SqlAnswersService(IDbConnection connection)
        {
            _connection = connection;
        }

        public int Add(int userId, int questionId, string message, string image)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userId";
            userIdParam.Value = userId;

            var questionIdParam = command.CreateParameter();
            questionIdParam.ParameterName = "questionId";
            questionIdParam.Value = questionId;

            var messageParam = command.CreateParameter();
            messageParam.ParameterName = "message";
            messageParam.Value = (object)message ?? DBNull.Value;

            var imageParam = command.CreateParameter();
            imageParam.ParameterName = "image";
            imageParam.Value = (object)image ?? DBNull.Value;

            command.CommandText = "INSERT INTO answer (user_id, question_id, message, image) VALUES (@userId, @questionId, @message, @image) RETURNING id";
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(questionIdParam);
            command.Parameters.Add(messageParam);
            command.Parameters.Add(imageParam);

            using var reader = command.ExecuteReader();
            reader.Read();
            return (int)reader["id"];
        }

        public void Delete(int id)
        {
            using var command = _connection.CreateCommand();

            var param = command.CreateParameter();
            param.ParameterName = "id";
            param.Value = id;

            command.CommandText = "DELETE FROM answer WHERE id = @id";
            command.Parameters.Add(param);

            command.ExecuteNonQuery();
        }

        public void DeleteAll(int questionId)
        {
            using var command = _connection.CreateCommand();

            var param = command.CreateParameter();
            param.ParameterName = "questionId";
            param.Value = questionId;

            command.CommandText = "DELETE FROM answer WHERE question_id = @questionId";
            command.Parameters.Add(param);

            command.ExecuteNonQuery();
        }

        public List<Answer> GetAll(IAnswersService.GetAllOptions opts)
        {
            using var command = _connection.CreateCommand();
            string sql = "SELECT * FROM answer";
            if (opts.UserId != null && opts.QuestionId != null)
            {
                throw new NotImplementedException();
            }
            if (opts.UserId != null)
            {
                sql += " WHERE user_id = @userId";
                var userIdParam = command.CreateParameter();
                userIdParam.ParameterName = "userId";
                userIdParam.Value = (int)opts.UserId;
                command.Parameters.Add(userIdParam);
            }
            if (opts.QuestionId != null)
            {
                sql += " WHERE question_id = @questionId";
                var questionIdParam = command.CreateParameter();
                questionIdParam.ParameterName = "questionId";
                questionIdParam.Value = (int)opts.QuestionId;
                command.Parameters.Add(questionIdParam);
            }
            sql += $" ORDER BY {opts.Sort.ToString().ToSnakeCase()} {(opts.Ascending ? "ASC" : "DESC")}";
            command.CommandText = sql;

            using var reader = command.ExecuteReader();
            List<Answer> answers = new List<Answer>();
            while (reader.Read())
            {
                answers.Add(ToAnswer(reader));
            }
            return answers;
        }

        public Answer GetOne(int id)
        {
            using var command = _connection.CreateCommand();

            var param = command.CreateParameter();
            param.ParameterName = "id";
            param.Value = id;

            command.CommandText = "SELECT * FROM answer WHERE id = @id";
            command.Parameters.Add(param);

            using var reader = command.ExecuteReader();
            reader.Read();
            return ToAnswer(reader);
        }

        public void Update(int id, string message, string image)
        {
            using var command = _connection.CreateCommand();

            var idParam = command.CreateParameter();
            idParam.ParameterName = "id";
            idParam.Value = id;

            var messageParam = command.CreateParameter();
            messageParam.ParameterName = "message";
            messageParam.Value = (object)message ?? DBNull.Value;

            var imageParam = command.CreateParameter();
            imageParam.ParameterName = "image";
            imageParam.Value = (object)image ?? DBNull.Value;

            command.CommandText = @"
                UPDATE
                    answer
                SET
                    message = @message,
                    image = @image
                WHERE
                    id = @id
            ";
            command.Parameters.Add(idParam);
            command.Parameters.Add(messageParam);
            command.Parameters.Add(imageParam);

            command.ExecuteNonQuery();
        }

        public void Vote(int id, int votes)
        {
            using var command = _connection.CreateCommand();

            var idParam = command.CreateParameter();
            idParam.ParameterName = "id";
            idParam.Value = id;

            var votesParam = command.CreateParameter();
            votesParam.ParameterName = "votes";
            votesParam.Value = votes;

            command.CommandText = "UPDATE answer SET vote_number = vote_number + @votes WHERE id = @id";
            command.Parameters.Add(idParam);
            command.Parameters.Add(votesParam);

            command.ExecuteNonQuery();
        }
    }
}
