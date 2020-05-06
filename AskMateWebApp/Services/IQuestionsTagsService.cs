namespace AskMateWebApp.Services
{
    public interface IQuestionsTagsService
    {
        void Add(int userId, int questionId, params int[] tagIds);
        void Delete(int userId, int questionId, params int[] tagIds);
        void DeleteAll(int questionId);
    }
}
