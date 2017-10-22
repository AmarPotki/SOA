using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;

namespace RahyabServices.Business.Services.Intefaces.Delinquent
{
    public interface ILogPrivilegeService {
        Task<bool> IsValidPrivilege(GetRequestClearingLogDto getRequestClearingLogDto);
        Task<bool> IsValidPrivilege(GetRequestSplitLogDto getRequestSplitLogDto);
        Task<bool> IsValidPrivilege(GetRequestGivingAChanceLogDto getRequestGivingAChanceLogDto);
        Task<bool> IsValidPrivilege(GetRequestImpunityLogDto getRequestImpunityLogDto);

    }
}
