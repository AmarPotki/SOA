using System;
using System.Threading.Tasks;
using FluentScheduler;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Business.Services.Intefaces.Supplies;
using RahyabServices.Common.Logging;
namespace TaskScheduler.Job{
    public class HrDocPermissionJob : IJob{
        private readonly ILogger _logger;
        private readonly IHrDocService _hrDocService;
        public HrDocPermissionJob(ILogger logger,  IHrDocService hrDocService)
        {
            _logger = logger;
            _hrDocService = hrDocService;
        }
        public void Execute(){
            try
            {
                 _hrDocService.CheckPermissionStates();
                _logger.Info(new FaultDto { Location = "Windows Service HrDocPermissionJob", Message = "Worked Done" });
            }
            catch (Exception ex)
            {
                _logger.Info(new FaultDto
                {
                    Location = "Windows Service HrDocPermissionJob",
                    Message = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }
    }
}