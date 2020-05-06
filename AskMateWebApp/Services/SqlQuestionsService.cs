using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace AskMateWebApp.Services
{
    public class SqlQuestionsService : IQuestionsService
    {
        private static Question ToQuestion(IDataReader reader)
        {
            return new Question
            {
                Id = (int)reader["id"],
                UserId = (int)reader["user_id"],
                SubmissionTime = (DateTime)reader["submission_time"],
                Message = reader["message"] as string,
                Title = reader["title"] as string,
                ViewNumber = (int)reader["view_number"],
                VoteNumber = (int)reader["vote_number"],
                Image = reader["image"] as string,
            };
        }

        private readonly IDbConnection _connection;

        public SqlQuestionsService(IDbConnection connection)
        {
            _connection = connection;
        }

        public int Add(int userId, string title, string message, string image)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userId";
            userIdParam.Value = userId;

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            var messageParam = command.CreateParameter();
            messageParam.ParameterName = "message";
            messageParam.Value = (object)message ?? DBNull.Value;

            var imageParam = command.CreateParameter();
            imageParam.ParameterName = "image";
            imageParam.Value = (object)image ?? DBNull.Value;

            command.CommandText = "INSERT INTO question (user_id, title, message, image) VALUES (@userId, @title, @message, @image) RETURNING id";
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(titleParam);
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

            command.CommandText = "DELETE FROM question WHERE id = @id";
            command.Parameters.Add(param);

            command.ExecuteNonQuery();
        }

        public List<Question> GetAll(IQuestionsService.GetAllOptions opts)
        {
            using var command = _connection.CreateCommand();
            string sql = "SELECT * FROM question";
            if (opts.UserId != null)
            {
                sql += " WHERE user_id = @userId";
                var userIdParam = command.CreateParameter();
                userIdParam.ParameterName = "userId";
                userIdParam.Value = (int)opts.UserId;
                command.Parameters.Add(userIdParam);
            }
            sql += $" ORDER BY {opts.Sort.ToString().ToSnakeCase()} {(opts.Ascending ? "ASC" : "DESC")}";
            if (opts.Limit != null)
            {
                sql += " LIMIT @limit";
                var limitParam = command.CreateParameter();
                limitParam.ParameterName = "limit";
                limitParam.Value = (int)opts.Limit;
                command.Parameters.Add(limitParam);
            }
            command.CommandText = sql;

            using var reader = command.ExecuteReader();
            List<Question> questions = new List<Question>();
            while (reader.Read())
            {
                questions.Add(ToQuestion(reader));
            }
            return questions;
        }

        public Question GetOne(int id)
        {
            using var command = _connection.CreateCommand();

            var param = command.CreateParameter();
            param.ParameterName = "id";
            param.Value = id;

            command.CommandText = "SELECT * FROM question WHERE id = @id";
            command.Parameters.Add(param);

            using var reader = command.ExecuteReader();
            reader.Read();
            return ToQuestion(reader);
        }

        public void Update(int id, string title, string message, string image)
        {
            using var command = _connection.CreateCommand();

            var idParam = command.CreateParameter();
            idParam.ParameterName = "id";
            idParam.Value = id;

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            var messageParam = command.CreateParameter();
            messageParam.ParameterName = "message";
            messageParam.Value = (object)message ?? DBNull.Value;

            var imageParam = command.CreateParameter();
            imageParam.ParameterName = "image";
            imageParam.Value = (object)image ?? DBNull.Value;

            command.CommandText = @"
                UPDATE
                    question
                SET
                    title = @title,
                    message = @message,
                    image = @image
                WHERE
                    id = @id
            ";
            command.Parameters.Add(idParam);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(messageParam);
            command.Parameters.Add(imageParam);

            command.ExecuteNonQuery();
        }

        public void View(int id)
        {
            using var command = _connection.CreateCommand();

            var param = command.CreateParameter();
            param.ParameterName = "id";
            param.Value = id;

            command.CommandText = "UPDATE question SET view_number = view_number + 1 WHERE id = @id";
            command.Parameters.Add(param);

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

            command.CommandText = "UPDATE question SET vote_number = vote_number + @votes WHERE id = @id";
            command.Parameters.Add(idParam);
            command.Parameters.Add(votesParam);

            command.ExecuteNonQuery();
        }
    }
}
