using AskMateWebApp.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        internal void appendTo(params object[] fields)
        {
            File.AppendAllText(_csvPath, string.Join(_delimiter, fields) + Environment.NewLine);
        }

        internal void updateAt(int id, params object[] fields)
        {
            var lines = File.ReadAllLines(_csvPath).Select(line =>
            {
                if (line.StartsWith(id + _delimiter))
                    return string.Join(_delimiter, fields);
                return line;
            });
            File.WriteAllLines(_csvPath, lines);
        }

        internal string[] readFrom(int id)
        {
            return File.ReadAllLines(_csvPath)
                .Where(x => x.StartsWith(id + _delimiter))
                .Select(x => x.Split(_delimiter))
                .Single();
        }

        internal List<string[]> readAllFrom()
        {
            return File.ReadAllLines(_csvPath)
                .Select(x => x.Split(_delimiter))
                .ToList();
        }
    }
}
