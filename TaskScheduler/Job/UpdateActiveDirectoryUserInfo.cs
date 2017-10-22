using System;
using System.Threading.Tasks;
using FluentScheduler;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Common.Logging;
namespace TaskScheduler.Job{
    public class UpdateActiveDirectoryUserInfo : IJob{
        private readonly IActiveDirectoryService _activeDirectoryService;
        private readonly ILogger _logger;
        public UpdateActiveDirectoryUserInfo(IActiveDirectoryService activeDirectoryService, ILogger logger){
            _activeDirectoryService = activeDirectoryService;
            _logger = logger;
        }
        public void Execute(){
            ExcuteAsync().Wait();
        }
        public async Task<bool> ExcuteAsync(){
            try{
                await _activeDirectoryService.UpdateActiveDirectoryGroups();
                await _activeDirectoryService.UpdateActiveDirectoryUsers();
                await _activeDirectoryService.UpdateDeActiveUsers();
                _logger.Info(new FaultDto
                {
                    Location = "Windows Service UpdateActiveDirectoryUserInfo",
                    Message = "Worked Don"
                });
            }
            catch (Exception ex){
                _logger.Info(new FaultDto
                {
                    Location = "Windows Service UpdateActiveDirectoryUserInfo",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
            return true;
        }
    }
}