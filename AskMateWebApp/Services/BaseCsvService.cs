using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AskMateWebApp.Services
{
    public abstract class BaseCsvService
    {
        private readonly string _delimiter = ";";
        private readonly string _csvPath;

        internal BaseCsvService(string csvPath)
        {
            _csvPath = csvPath;
        }
        internal void AppendTo(params object[] fields)
        {
            File.AppendAllText(_csvPath, string.Join(_delimiter, fields) + Environment.NewLine);
        }

        internal void UpdateAt(int id, params object[] fields)
        {
            var lines = File.ReadAllLines(_csvPath).Select(line =>
            {
                if (line.StartsWith(id + _delimiter))
                    return string.Join(_delimiter, fields);
                return line;
            });
            File.WriteAllLines(_csvPath, lines);
        }

        internal void DeleteAt(int id)
        {
            DeleteAt(fields => !fields[0].Equals(id.ToString()));
        }

        internal void DeleteAt(Func<string[], bool> predicate)
        {
            var lines = File.ReadAllLines(_csvPath).Where(line => predicate(line.Split(_delimiter)));
            File.WriteAllLines(_csvPath, lines);
        }

        internal string[] ReadFrom(int id)
        {
            return File.ReadAllLines(_csvPath)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Where(x => x.StartsWith(id + _delimiter))
                .Select(x => x.Split(_delimiter))
                .Single();
        }

        internal List<string[]> ReadAllFrom()
        {
            return File.ReadAllLines(_csvPath)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Split(_delimiter))
                .ToList();
        }
    }
}
