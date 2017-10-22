using Autofac;
using FluentScheduler;
namespace TaskScheduler.Job{
    public class JobFactory :IJobFactory{
        public IJob GetJobInstance<T>() where T : IJob{
            var container = IoC.InitializeBusiness();
            return container.Resolve<T>();
        }
    }
}