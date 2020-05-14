namespace AskMateCommon.Services
{
    public interface IDatabaseService
    {
        bool Initialized { get; }
        void Reset();
    }
}
