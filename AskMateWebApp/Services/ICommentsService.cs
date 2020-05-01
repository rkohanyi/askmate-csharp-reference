using AskMateWebApp.Domain;
using System.Collections.Generic;

namespace AskMateWebApp.Services
{
    public interface ICommentsService
    {
        public enum CommentType
        {
            Question,
            Answer
        }

        Comment GetOne(int id);
        List<Comment> GetAll(CommentType type, int parentId);
        Dictionary<int, List<Comment>> GetAll(CommentType type, params int[] parentIds);

        int Add(CommentType type, int parentId, string message);
        void Update(int id, string message);
        void Delete(int id);
        void DeleteAll(CommentType type, int parentId);
    }
}
