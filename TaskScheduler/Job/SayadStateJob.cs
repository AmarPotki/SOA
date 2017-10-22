using System;
using System.Threading.Tasks;
using FluentScheduler;
using RahyabServices.Business.Services.Intefaces.Supplies;
using RahyabServices.Common.Logging;
namespace TaskScheduler.Job{
    public class SayadStateJob : IJob{
        private readonly ILogger _logger;
        private readonly ISuppliesService _suppliesService;
        public SayadStateJob(ILogger logger, ISuppliesService suppliesService){
            _logger = logger;
            _suppliesService = suppliesService;
        }
        public void Execute(){
            ExcuteAsync().Wait();
        }
        public async Task<bool> ExcuteAsync(){
            try{
                await _suppliesService.CheckSayadState();
                _logger.Info(new FaultDto {Location = "Windows Service SayadStateJob", Message = "Worked Don"});
            }
            catch (Exception ex) {}
            return true;
        }
    }
}