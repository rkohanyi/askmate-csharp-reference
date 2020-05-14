using AskMate.Common.Domain;
using System.Collections.Generic;

namespace AskMate.Common.Services
{
    public interface ITagsService
    {
        List<Tag> GetAll();
        List<Tag> GetAll(int questionId);
        List<int> Add(params string[] names);
    }
}
