using System;
using System.Threading.Tasks;
using FluentScheduler;
using RahyabServices.Business.Services.Intefaces.Supplies;
using RahyabServices.Common.Logging;
namespace TaskScheduler.Job{
    public class IranNaraJob:IJob{
        private readonly ILogger _logger;
        private readonly ISuppliesService _suppliesService;
        public IranNaraJob(ILogger logger, ISuppliesService suppliesService)
        {
            _logger = logger;
            _suppliesService = suppliesService;
        }
        public void Execute(){
            ExcuteAsync().Wait();
        }
        public async Task<bool> ExcuteAsync()
        {
            try
            {
               await _suppliesService.CheckIranNaraState();
                _logger.Info(new FaultDto { Location = "Windows Service IranNaraJob", Message = "Worked Don" });
            }
            catch (Exception ex)
            {
                _logger.Info(new FaultDto
                {
                    Location = "Windows Service IranNaraJob",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
            return true;
        }
    }
}