using System.IO;

namespace AskMateCommon.Services
{
    public interface IStorageService
    {
        string Save(string fileName, Stream stream);
        void Delete(string fileName);
    }
}
