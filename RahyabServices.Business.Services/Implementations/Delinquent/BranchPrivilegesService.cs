using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;
using RahyabServices.Business.Dtos.Delinquent.Log.Renewal;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;
using RahyabServices.Business.Services.Intefaces.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Delinquent{
    public class BranchPrivilegesService : IBranchPrivilegesService{
        private readonly IBranchRepository _branchRepository;
        public BranchPrivilegesService(IBranchRepository branchRepository){
            _branchRepository = branchRepository;
        }
        public async Task<bool> IsValidPrivilege(string branchCode, RequestType requestType){
            if (requestType == RequestType.Clearing) return await IsValidForClearing(branchCode);
            if (requestType == RequestType.Impunity) return await IsValidForImpunity(branchCode);
            return await IsValidForSplit(branchCode);
        }
        public async Task<bool> IsValidPrivilege(string branchCode, AddSplitLogDto addSplitLogDto){
            var branch = await _branchRepository.GetBranchByCode(branchCode);
            if (branch.BranchRate == BranchRate.Center) { if (addSplitLogDto.Count > 12) return false; }
            if (branch.BranchRate == BranchRate.Great) { if (addSplitLogDto.Count > 8) return false; }
            if (branch.BranchRate == BranchRate.One || branch.BranchRate == BranchRate.Two)
                if (addSplitLogDto.Count > 6) return false;
            if (branch.BranchRate == BranchRate.Three || branch.BranchRate == BranchRate.Four) 
                if (addSplitLogDto.Count > 4) return false;
            return true;
        }

        public async Task<bool> IsValidPrivilege(string branchCode, EditSplitLogDto editSplitLogDto) {
            var branch = await _branchRepository.GetBranchByCode(branchCode);
            if (branch.BranchRate == BranchRate.Center) { if (editSplitLogDto.Count > 12) return false; }
            if (branch.BranchRate == BranchRate.Great) { if (editSplitLogDto.Count > 8) return false; }
            if (branch.BranchRate == BranchRate.One || branch.BranchRate == BranchRate.Two)
                if (editSplitLogDto.Count > 6) return false;
            if (branch.BranchRate == BranchRate.Three || branch.BranchRate == BranchRate.Four)
                if (editSplitLogDto.Count > 4) return false;
            return true;
        }

        public async Task<bool> IsValidPrivilege(string branchCode, AddImpunityForCrimesLogDto addImpunityForCrimesLogDto)
        {
            return false;
        }

        public async Task<bool> IsValidPrivilege(string branchCode, EditImpunityForCrimesLogDto editImpunityForCrimesLogDto)
        {
            return false;
        }

        public async Task<bool> IsValidPrivilege(string branchCode, EditRenewalLogDto editRenewalLogDto)
        {
            return true;
        }
        public async Task<bool> IsValidPrivilege(string branchCode, AddRenewalLogDto addRenewalLogDto)
        {
            return true;
        }
        public async Task<bool> IsValidPrivilege(string branchCode, AddClearingLogDto addClearingLogDto){
            return false;
        }
        public async Task<bool> IsValidPrivilege(string branchCode, EditClearingLogDto editClearingLogDto)
        {
            return false;
        }
        public async Task<IEnumerable<Branch>> GetAllAsync(){
            return  await _branchRepository.GetAllAsNoTracking();
        }
        public async Task<bool> IsValidPrivilege(string branchCode, AddGivingAChanceLogDto addGivingAChanceLogDto){
            var branch = await _branchRepository.GetBranchByCode(branchCode);
            if (branch.BranchRate == BranchRate.Center) { if (addGivingAChanceLogDto.Count > 4) return false; }
            if (branch.BranchRate == BranchRate.Great) { if (addGivingAChanceLogDto.Count > 3) return false; }
            if (branch.BranchRate == BranchRate.One || branch.BranchRate == BranchRate.Two)
                if (addGivingAChanceLogDto.Count > 2) return false;
            if (branch.BranchRate == BranchRate.Three || branch.BranchRate == BranchRate.Four)
                if (addGivingAChanceLogDto.Count > 1) return false;
            return true;
        }

        public async Task<bool> IsValidPrivilege(string branchCode, EditGivingAChanceLogDto editGivingAChanceLogDto)
        {
            var branch = await _branchRepository.GetBranchByCode(branchCode);
            if (branch.BranchRate == BranchRate.Center) { if (editGivingAChanceLogDto.Count > 4) return false; }
            if (branch.BranchRate == BranchRate.Great) { if (editGivingAChanceLogDto.Count > 3) return false; }
            if (branch.BranchRate == BranchRate.One || branch.BranchRate == BranchRate.Two)
                if (editGivingAChanceLogDto.Count > 2) return false;
            if (branch.BranchRate == BranchRate.Three || branch.BranchRate == BranchRate.Four)
                if (editGivingAChanceLogDto.Count > 1) return false;
            return true;
        }

        protected async Task<bool> IsValidForClearing(string branchCode){
            return false;
        }
        protected async Task<bool> IsValidForImpunity(string branchCode){
            return false;
        }
        protected async Task<bool> IsValidForSplit(string branchCode){
            return false;
        }
    }
} 