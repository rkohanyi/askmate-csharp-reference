using AskMateWebApp.Domain;
using System.Collections.Generic;

namespace AskMateWebApp.Services
{
    public interface ITagsService
    {
        List<Tag> GetAll();
        List<Tag> GetAll(int questionId);
        List<int> Add(params string[] names);
    }
}
