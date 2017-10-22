using System.ServiceProcess;
using FluentScheduler;
using RahyabServices.Common.Logging;
using TaskScheduler.Job;
using Autofac;
using IContainer = Autofac.IContainer;
namespace TaskScheduler
{
    public partial class RahyabScheduler : ServiceBase
    {
        private readonly ILogger _logger;
        private readonly IContainer _container;
        public RahyabScheduler()
        {
            _container = IoC.InitializeBusiness();
            var bootstrapper = _container.Resolve<Bootstrapper>();
            bootstrapper.Initialize();
            _logger = _container.Resolve<ILogger>();
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
      

            JobManager.JobFactory = new JobFactory();
            JobManager.Initialize(new SchedulerRegister());
        }

        protected override void OnStop()
        {
        }
        public void StartDebug()
        {
            OnStart(null);
        }
    }
}
