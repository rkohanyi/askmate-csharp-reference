using AskMate.Common.Domain;
using System;
using System.Collections.Generic;
using System.Data;

namespace AskMate.Common.Services
{
    public class SqlUsersService : SqlBaseService, IUsersService
    {
        private static User ToUser(IDataReader reader)
        {
            return new User
            {
                Id = (int)reader["id"],
                Username = (string)reader["username"],
                RegistrationTime = (DateTime)reader["registration_time"],
                QuestionCount = (long)reader["question_count"],
                AnswerCount = (long)reader["answer_count"],
                CommentCount = (long)reader["comment_count"],
            };
        }

        private readonly IDbConnection _connection;

        public SqlUsersService(IDbConnection connection)
        {
            _connection = connection;
        }

        public User GetOne(int id)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM v_user WHERE id = @id";

            var param = command.CreateParameter();
            param.ParameterName = "id";
            param.Value = id;

            using var reader = command.ExecuteReader();
            reader.Read();
            return ToUser(reader);
        }

        public List<User> GetAll()
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM v_user";

            using var reader = command.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                users.Add(ToUser(reader));
            }
            return users;
        }

        public User Login(string username, string password)
        {
            using var command = _connection.CreateCommand();

            var usernameParam = command.CreateParameter();
            usernameParam.ParameterName = "username";
            usernameParam.Value = username;

            var passwordParam = command.CreateParameter();
            passwordParam.ParameterName = "password";
            passwordParam.Value = password;

            command.CommandText = @"SELECT * FROM v_user WHERE username = @username AND password = digest(@password, 'sha512')";
            command.Parameters.Add(usernameParam);
            command.Parameters.Add(passwordParam);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return ToUser(reader);
            }
            return null;
        }

        public void Register(string username, string password)
        {
            using var command = _connection.CreateCommand();

            var usernameParam = command.CreateParameter();
            usernameParam.ParameterName = "username";
            usernameParam.Value = username;

            var passwordParam = command.CreateParameter();
            passwordParam.ParameterName = "password";
            passwordParam.Value = password;

            command.CommandText = @"INSERT INTO ""user"" (username, password) VALUES (@username, digest(@password, 'sha512'))";
            command.Parameters.Add(usernameParam);
            command.Parameters.Add(passwordParam);

            HandleExecuteNonQuery(command);
        }
    }
}
