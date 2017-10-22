using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace RahyabServices.ServiceTest
{
    public class Bootstrapper
    {
        private readonly IEnumerable<IBootstrapTask> _tasks;

        public Bootstrapper(IEnumerable<IBootstrapTask> tasks)
        {
            _tasks = tasks;
        }

        public void Initialize()
        {
            foreach (var bootstrapTask in _tasks.OrderBy(t => t.Priority))
            {
                bootstrapTask.Execute();
            }
        }

        public static void ConfigureApplication(IContainer container)
        {
            var bootstrapper = container.Resolve<Bootstrapper>();
            bootstrapper.Initialize();
        }
    }
}
