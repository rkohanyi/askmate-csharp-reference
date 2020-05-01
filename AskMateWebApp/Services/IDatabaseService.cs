namespace AskMateWebApp.Services
{
    public interface IDatabaseService
    {
        bool Initialized { get; }
        void Reset();
    }
}
