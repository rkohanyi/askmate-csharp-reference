using AskMateWebApp.Domain;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AskMateWebApp.Services
{
    public class SqlTagsService : ITagsService
    {
        private static Tag ToTag(IDataReader reader)
        {
            return new Tag
            {
                Id = (int)reader["id"],
                Name = (string)reader["name"],
            };
        }

        private readonly IDbConnection _connection;

        public SqlTagsService(IDbConnection connection)
        {
            this._connection = connection;
        }

        public List<Tag> GetAll()
        {
            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM tag";

            using var reader = command.ExecuteReader();
            List<Tag> tags = new List<Tag>();
            while (reader.Read())
            {
                tags.Add(ToTag(reader));
            }
            return tags;
        }

        public List<Tag> GetAll(int questionId)
        {
            using var command = _connection.CreateCommand();
            command.CommandText = $@"
                SELECT
                    t.*
                FROM
                    tag AS t
                JOIN
                    question_tag AS qt ON qt.tag_id = t.id
                WHERE
                    qt.question_id = @questionId
            ";

            var param = command.CreateParameter();
            param.ParameterName = "questionId";
            param.Value = questionId;
            command.Parameters.Add(param);

            using var reader = command.ExecuteReader();
            List<Tag> tags = new List<Tag>();
            while (reader.Read())
            {
                tags.Add(ToTag(reader));
            }
            return tags;
        }

        public List<int> Add(params string[] names)
        {
            using var command = _connection.CreateCommand();
            // NOTE: https://stackoverflow.com/a/35265559/433835
            command.CommandText = $@"
                WITH names (name) AS (
                    VALUES {string.Join(", ", names.Select((_, i) => $"(@name{i})"))}
                ),
                inserted_names (id, name) AS (
                    INSERT INTO tag (name) SELECT name FROM names ON CONFLICT DO NOTHING RETURNING id, name
                )
                SELECT tag.id FROM tag JOIN names ON names.name = tag.name
                UNION ALL
                SELECT id FROM inserted_names
            ";

            for (int i = 0; i < names.Length; i++)
            {
                var param = command.CreateParameter();
                param.ParameterName = $"name{i}";
                param.Value = names[i];
                command.Parameters.Add(param);
            }

            using var reader = command.ExecuteReader();

            List<int> ids = new List<int>();
            while (reader.Read())
            {
                ids.Add((int)reader["id"]);
            }
            return ids;
        }
    }
}
