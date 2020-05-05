using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace AskMateWebApp.Services
{
    public class SqlCommentsService : ICommentsService
    {
        private static Comment ToComment(IDataReader reader)
        {
            return new Comment
            {
                Id = (int)reader["id"],
                QuestionId = reader["question_id"] as int? ?? 0,
                AnswerId = reader["answer_id"] as int? ?? 0,
                SubmissionTime = (DateTime)reader["submission_time"],
                EditedNumber = (int)reader["edited_number"],
                Message = reader["message"] as string,
            };
        }

        private readonly IDbConnection _connection;

        public SqlCommentsService(IDbConnection connection)
        {
            this._connection = connection;
        }

        public int Add(int userId, ICommentsService.CommentType type, int parentId, string message)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userId";
            userIdParam.Value = userId;

            var parentIdParam = command.CreateParameter();
            parentIdParam.ParameterName = "parentId";
            parentIdParam.Value = parentId;

            var messageParam = command.CreateParameter();
            messageParam.ParameterName = "message";
            messageParam.Value = (object)message ?? DBNull.Value;

            command.CommandText = @$"
                INSERT INTO comment (
                    user_id,
                    {type.ToString().ToSnakeCase() + "_id"},
                    message
                )
                VALUES (
                    @userId,
                    @parentId,
                    @message
                )
                RETURNING id
            ";
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(parentIdParam);
            command.Parameters.Add(messageParam);

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

            command.CommandText = "DELETE FROM comment WHERE id = @id";
            command.Parameters.Add(param);

            command.ExecuteNonQuery();
        }

        public void DeleteAll(ICommentsService.CommentType type, int parentId)
        {
            using var command = _connection.CreateCommand();

            var param = command.CreateParameter();
            param.ParameterName = "parentId";
            param.Value = parentId;

            command.CommandText = $"DELETE FROM comment WHERE {type.ToString().ToSnakeCase() + "_id"} = @parentId";
            command.Parameters.Add(param);

            command.ExecuteNonQuery();
        }

        public List<Comment> GetAll(int userId)
        {
            using var command = _connection.CreateCommand();
            command.CommandText += "SELECT * FROM comment WHERE user_id = @userId ORDER BY submission_time DESC";
            var param = command.CreateParameter();
            param.ParameterName = "userId";
            param.Value = userId;
            command.Parameters.Add(param);

            using var reader = command.ExecuteReader();
            List<Comment> comments = new List<Comment>();
            while (reader.Read())
            {
                comments.Add(ToComment(reader));
            }
            return comments;
        }

        public List<Comment> GetAll(ICommentsService.CommentType type, int parentId)
        {
            return GetAll(type, new int[] { parentId })[parentId];
        }

        public Dictionary<int, List<Comment>> GetAll(ICommentsService.CommentType type, params int[] parentIds)
        {
            if (parentIds.Length == 0)
            {
                return new Dictionary<int, List<Comment>>();
            }

            using var command = _connection.CreateCommand();

            var sql = $"SELECT * FROM comment WHERE {type.ToString().ToSnakeCase() + "_id"} IN (";
            for (int i = 0; i < parentIds.Length; i++)
            {
                sql += $"@parentId{i}";
                if (i < parentIds.Length - 1)
                {
                    sql += ",";
                }
            }
            sql += ") ORDER BY submission_time DESC";
            command.CommandText = sql;

            for (int i = 0; i < parentIds.Length; i++)
            {
                var param = command.CreateParameter();
                param.ParameterName = $"parentId{i}";
                param.Value = parentIds[i];
                command.Parameters.Add(param);
            }

            using var reader = command.ExecuteReader();
            Dictionary<int, List<Comment>> comments = new Dictionary<int, List<Comment>>();
            foreach (var parentId in parentIds)
            {
                comments[parentId] = new List<Comment>();
            }
            while (reader.Read())
            {
                var comment = ToComment(reader);
                int parentId = type switch
                {
                    ICommentsService.CommentType.Question => comment.QuestionId,
                    _ => (int)comment.AnswerId
                };
                comments[parentId].Add(comment);
            }
            return comments;
        }

        public Comment GetOne(int id)
        {
            using var command = _connection.CreateCommand();

            var param = command.CreateParameter();
            param.ParameterName = "id";
            param.Value = id;

            command.CommandText = "SELECT * FROM comment WHERE id = @id";
            command.Parameters.Add(param);

            using var reader = command.ExecuteReader();
            reader.Read();
            return ToComment(reader);
        }

        public void Update(int id, string message)
        {
            using var command = _connection.CreateCommand();

            var idParam = command.CreateParameter();
            idParam.ParameterName = "id";
            idParam.Value = id;

            var messageParam = command.CreateParameter();
            messageParam.ParameterName = "message";
            messageParam.Value = (object)message ?? DBNull.Value;

            command.CommandText = @"
                UPDATE
                    comment
                SET
                    message = @message,
                    edited_number = edited_number + 1,
                    submission_time = NOW()
                WHERE
                    id = @id
            ";
            command.Parameters.Add(idParam);
            command.Parameters.Add(messageParam);

            command.ExecuteNonQuery();
        }
    }
}
