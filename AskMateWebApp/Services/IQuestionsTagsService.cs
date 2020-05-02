namespace AskMateWebApp.Services
{
    public interface IQuestionsTagsService
    {
        void Add(int questionId, params int[] tagIds);
        void Delete(int questionId, params int[] tagIds);
        void DeleteAll(int questionId);
    }
}
