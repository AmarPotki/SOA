using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using RahyabServices.Business.Services.Intefaces.BranchMarketing;
using RahyabServices.Common.Extensions;
using RahyabServices.Common.Logging;
namespace TaskScheduler.Job
{
   public class BranchMarketingCheckItemJob : IJob
    
    {
        private readonly IBranchMarketingService _branchMarketingService;
        private readonly ILogger _logger;
       public BranchMarketingCheckItemJob(ILogger logger, IBranchMarketingService branchMarketingService){
           _logger = logger;
           _branchMarketingService = branchMarketingService;
       }
       public void Execute(){
            ExecuteAsync().Wait();
        }
       private async Task ExecuteAsync(){
            try
            {
                _logger.Info(new FaultDto { Location = "Windows Service BranchMarketingCheckSuccessCustomersJob", Message = "start" });
                await _branchMarketingService.CheckSuccessCustomers();
                _logger.Info(new FaultDto { Location = "Windows Service BranchMarketingCheckSuccessCustomersJob", Message = "Worked Done" });

            }
            catch (Exception ex)
            {

                _logger.Info(new FaultDto
                {
                    Location = "Windows Service BranchMarketingCheckSuccessCustomersJob",
                    Message = ex.GetMessage(),
                    StackTrace = ex.StackTrace
                });
            }
        }
   }
}
