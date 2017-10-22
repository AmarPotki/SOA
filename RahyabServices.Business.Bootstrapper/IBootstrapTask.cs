namespace RahyabServices.Business.Bootstrapper
{
    public interface IBootstrapTask
    {
        void Execute();
        int Priority { get; }
    }
}
