namespace RahyabServices.ServiceTest
{
    public interface IBootstrapTask
    {
        void Execute();
        int Priority { get; }
    }
}
