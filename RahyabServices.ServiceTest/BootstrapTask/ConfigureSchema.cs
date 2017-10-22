using RahyabServices.DataAccess.Core;

namespace RahyabServices.Business.Bootstrapper.BootstrapTask
{
    public class ConfigureSchema : IBootstrapTask
    {
        public int Priority
        {
            get { return 3; }
        }

        public void Execute()
        {
            new SchemaSynchronizer().Execute();
        }
    }
}
