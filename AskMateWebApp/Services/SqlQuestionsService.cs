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
            this._connection = connection;
        }

        public int Add(string title, string message, string image)
        {
            using var command = _connection.CreateCommand();

            var titleParam = command.CreateParameter();
            titleParam.ParameterName = "title";
            titleParam.Value = (object)title ?? DBNull.Value;

            var messageParam = command.CreateParameter();
            messageParam.ParameterName = "message";
            messageParam.Value = (object)message ?? DBNull.Value;

            var imageParam = command.CreateParameter();
            imageParam.ParameterName = "image";
            imageParam.Value = (object)image ?? DBNull.Value;

            command.CommandText = "INSERT INTO question (title, message, image) VALUES (@title, @message, @image) RETURNING id";
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

        public List<Question> GetAll()
        {
            return GetAll(Question.SortField.SubmissionTime, false, 0);
        }

        public List<Question> GetAll(long limit)
        {
            return GetAll(Question.SortField.SubmissionTime, false, limit);
        }

        public List<Question> GetAll(bool ascending)
        {
            return GetAll(Question.SortField.SubmissionTime, ascending, 0);
        }

        public List<Question> GetAll(bool ascending, long limit)
        {
            return GetAll(Question.SortField.SubmissionTime, ascending, limit);
        }

        public List<Question> GetAll(Question.SortField sort)
        {
            return GetAll(sort, false, 0);
        }

        public List<Question> GetAll(Question.SortField sort, long limit)
        {
            return GetAll(sort, false, limit);
        }

        public List<Question> GetAll(Question.SortField sort, bool ascending)
        {
            return GetAll(sort, false, 0);
        }

        public List<Question> GetAll(Question.SortField sort, bool ascending, long limit)
        {
            using var command = _connection.CreateCommand();
            string sql = $"SELECT * FROM question ORDER BY {sort.ToString().ToSnakeCase()} {(ascending ? "ASC" : "DESC")}";
            if (limit > 0)
            {
                sql += $" LIMIT {limit}";
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
