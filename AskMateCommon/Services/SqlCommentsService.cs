using AskMate.Common.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace AskMate.Common.Services
{
    public class SqlCommentsService : SqlBaseService, ICommentsService
    {
        private static Comment ToComment(IDataReader reader)
        {
            return new Comment
            {
                Id = (int)reader["id"],
                UserId = (int)reader["user_id"],
                QuestionId = (int)reader["question_id"],
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

        public void Delete(int userId, int id)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userId";
            userIdParam.Value = userId;

            var idParam = command.CreateParameter();
            idParam.ParameterName = "id";
            idParam.Value = id;

            command.CommandText = "SELECT delete_comment(@userId, @id)";
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(idParam);

            HandleExecuteNonQuery(command);
        }

        public void DeleteAll(int userId, ICommentsService.CommentType type, int parentId)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userId";
            userIdParam.Value = userId;

            var parentIdParam = command.CreateParameter();
            parentIdParam.ParameterName = "parentId";
            parentIdParam.Value = parentId;

            command.CommandText = $"SELECT delete_all_{type.ToString().ToSnakeCase()}_comment(@userId, @parentId)";
            command.Parameters.Add(parentIdParam);
            command.Parameters.Add(userIdParam);

            HandleExecuteNonQuery(command);
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

        public void Update(int userId, int id, string message)
        {
            using var command = _connection.CreateCommand();

            var userIdParam = command.CreateParameter();
            userIdParam.ParameterName = "userId";
            userIdParam.Value = userId;

            var idParam = command.CreateParameter();
            idParam.ParameterName = "id";
            idParam.Value = id;

            var messageParam = command.CreateParameter();
            messageParam.ParameterName = "message";
            messageParam.Value = (object)message ?? DBNull.Value;

            command.CommandText = @"SELECT update_comment(@userId, @id, @message)";
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(idParam);
            command.Parameters.Add(messageParam);

            HandleExecuteNonQuery(command);
        }
    }
}
