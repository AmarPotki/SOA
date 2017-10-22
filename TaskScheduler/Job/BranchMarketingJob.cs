using System;
using System.Threading.Tasks;
using FluentScheduler;
using RahyabServices.Business.Services.Intefaces.BranchMarketing;
using RahyabServices.Common.Extensions;
using RahyabServices.Common.Logging;
namespace TaskScheduler.Job{
    public class BranchMarketingJob : IJob{
        private readonly IBranchMarketingService _branchMarketingService;
        private readonly ILogger _logger;
        public BranchMarketingJob(IBranchMarketingService branchMarketingService, ILogger logger){
            _branchMarketingService = branchMarketingService;
            _logger = logger;
        }
        public void Execute(){
            ExecuteAsync().Wait();
        }
        public async Task ExecuteAsync()
        {
            try{
                _logger.Info(new FaultDto { Location = "Windows Service BranchMarketingJob", Message = "startet" });
                await _branchMarketingService.GetAndSaveItems();
                _logger.Info(new FaultDto { Location = "Windows Service BranchMarketingJob", Message = "Worked Done" });

            }
            catch (Exception ex){

                _logger.Info(new FaultDto
                {
                    Location = "Windows Service BranchMarketingJob",
                    Message = ex.GetMessage(),
                    StackTrace = ex.StackTrace
                });
            }
        }
    }
}