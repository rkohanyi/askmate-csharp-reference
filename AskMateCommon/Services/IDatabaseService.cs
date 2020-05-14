namespace AskMate.Common.Services
{
    public interface IDatabaseService
    {
        bool Initialized { get; }
        void Reset();
    }
}
