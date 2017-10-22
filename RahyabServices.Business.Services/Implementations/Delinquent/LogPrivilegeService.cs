using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;

namespace RahyabServices.Business.Services.Implementations.Delinquent
{
    public class LogPrivilegeService:ILogPrivilegeService {
        private readonly ILogBaseRepository _logBaseRepository;

        public LogPrivilegeService(ILogBaseRepository logBaseRepository) {
            _logBaseRepository = logBaseRepository;
        }

        public async Task<bool> IsValidPrivilege(GetRequestClearingLogDto getRequestClearingLogDto)
        {
            var clearingLog = await _logBaseRepository.OneAsync(getRequestClearingLogDto.RequestId);
            RequestClearingLog log = (RequestClearingLog)clearingLog;
            return log.AllowEdit;
        }

        public async Task<bool> IsValidPrivilege(GetRequestSplitLogDto getRequestSplitLogDto)
        {
            var splitLog = await _logBaseRepository.OneAsync(getRequestSplitLogDto.RequestId);
            RequestSplitLog log = (RequestSplitLog)splitLog;
            return log.AllowEdit;
        }
        public async Task<bool> IsValidPrivilege(GetRequestGivingAChanceLogDto getRequestGivingAChanceLogDto)
        {
            var givingAChanceLog = await _logBaseRepository.OneAsync(getRequestGivingAChanceLogDto.RequestId);
            RequestGivingAChanceLog log = (RequestGivingAChanceLog)givingAChanceLog;
            return log.AllowEdit;
        }
        public async Task<bool> IsValidPrivilege(GetRequestImpunityLogDto getRequestImpunityLogDto)
        {
            var impunityLog = await _logBaseRepository.OneAsync(getRequestImpunityLogDto.RequestId);
            RequestImpunityForCrimesLog log = (RequestImpunityForCrimesLog)impunityLog;
            return log.AllowEdit;
        }

    }
}
