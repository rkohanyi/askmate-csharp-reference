using System.IO;

namespace AskMateWebApp.Services
{
    public interface IStorageService
    {
        string Save(string fileName, Stream stream);
        void Delete(string fileName);
    }
}
