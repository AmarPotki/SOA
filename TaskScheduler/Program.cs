using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskScheduler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG

            var rahyabService = new RahyabScheduler();
            rahyabService.StartDebug();
            Thread.Sleep(Timeout.Infinite);

#endif
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new RahyabScheduler()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
