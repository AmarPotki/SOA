using RahyabServices.DataAccess.Core;

namespace TaskScheduler.BootstrapTask
{
    public class ConfigureSchema : IBootstrapTask
    {
        public int Priority =>2;
        public void Execute()
        {
            new SchemaSynchronizer().Execute();
        }
    }
}
