using Microsoft.Extensions.Logging;
using System.Data;

namespace AskMateWebApp.Services
{
    class PostgreSqlDatabaseService : IDatabaseService
    {
        private readonly ILogger<PostgreSqlDatabaseService> _logger;
        private readonly IDbConnection _connection;

        public PostgreSqlDatabaseService(ILogger<PostgreSqlDatabaseService> logger, IDbConnection connection)
        {
            _logger = logger;
            _connection = connection;
        }

        public bool Initialized
        {
            get
            {
                using var command = _connection.CreateCommand();
                command.CommandText = @"
                    SELECT EXISTS (
                        SELECT *
                        FROM information_schema.tables
                        WHERE table_name = 'comment'
                    )
                ";
                return (bool)command.ExecuteScalar();
            }
        }

        public void Reset()
        {
            foreach (string script in new string[] { "drop-init", "question", "answer", "tag", "question_tag", "comment", "full-text-search" })
            {
                _logger.LogDebug($"About to run {script}.sql");
                using IDbCommand command = _connection.CreateCommand();
                command.CommandText = System.IO.File.ReadAllText($"{script}.sql");
                command.ExecuteNonQuery();
                _logger.LogDebug($"Successfully run {script}.sql");
            }
        }
    }
}
