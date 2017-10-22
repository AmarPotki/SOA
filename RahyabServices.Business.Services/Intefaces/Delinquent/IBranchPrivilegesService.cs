using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Dtos.Delinquent.Log.Renewal;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;

namespace RahyabServices.Business.Services.Intefaces.Delinquent{
    public interface IBranchPrivilegesService{
        Task<bool> IsValidPrivilege(string branchCode ,RequestType requestType);
        Task<bool> IsValidPrivilege(string branchCode, AddSplitLogDto addSplitLogDto);
        Task<bool> IsValidPrivilege(string branchCode, EditSplitLogDto editSplitLogDto);
        Task<bool> IsValidPrivilege(string branchCode, AddClearingLogDto addClearingLogDto);
        Task<bool> IsValidPrivilege(string branchCode, AddGivingAChanceLogDto addGivingAChanceLogDto);
        Task<bool> IsValidPrivilege(string branchCode, EditGivingAChanceLogDto editGivingAChanceLogDto);
        Task<bool> IsValidPrivilege(string branchCode, EditClearingLogDto editClearingLogDto);
        Task<bool> IsValidPrivilege(string branchCode, AddImpunityForCrimesLogDto addImpunityForCrimesLogDto);
        Task<bool> IsValidPrivilege(string branchCode, EditImpunityForCrimesLogDto editImpunityForCrimesLogDto);
        Task<bool> IsValidPrivilege(string branchCode, EditRenewalLogDto editRenewalLogDto);
        Task<bool> IsValidPrivilege(string branchCode, AddRenewalLogDto addRenewalLogDto);
        Task<IEnumerable<Branch>> GetAllAsync();
    }
}