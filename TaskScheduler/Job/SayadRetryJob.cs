using System;
using System.Threading.Tasks;
using FluentScheduler;
using RahyabServices.Business.Services.Intefaces.Supplies;
using RahyabServices.Common.Logging;
namespace TaskScheduler.Job{
    public class SayadRetryJob : IJob
    {
        private readonly ILogger _logger;
        private readonly ISuppliesService _suppliesService;
        public SayadRetryJob(ILogger logger, ISuppliesService suppliesService)
        {
            _logger = logger;
            _suppliesService = suppliesService;
        }
        public void Execute()
        {
            ExcuteAsync().Wait();
        }
        public async Task<bool> ExcuteAsync()
        {
            try
            {
                await _suppliesService.RetryInquiry();
                _logger.Info(new FaultDto { Location = "Windows Service SayadRetryJob", Message = "Worked Don" });
            }
            catch (Exception ex)
            {
                _logger.Info(new FaultDto
                {
                    Location = "Windows Service SayadRetryJob",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
            return true;
        }

    }
}