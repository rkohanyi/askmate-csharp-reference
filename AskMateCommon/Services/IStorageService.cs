using System.IO;

namespace AskMate.Common.Services
{
    public interface IStorageService
    {
        string Save(string fileName, Stream stream);
        void Delete(string fileName);
    }
}
