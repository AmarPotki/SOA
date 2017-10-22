namespace TaskScheduler
{
    public interface IBootstrapTask
    {
        void Execute();
        int Priority { get; }
    }
}
