using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.BankPerson;
using RahyabServices.Business.Dtos.Delinquent.Branch;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Services.Intefaces.Bank;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class HrRestContract : ContractBase, IHrRestContract{
        private readonly IHrService _hrService;
        public HrRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer, ILogger logger, ISharepointAuthorizationService sharepointAuthorizationService, IHrService hrService) : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _hrService = hrService;
        }
        public async Task<PersonInfoDto> GetUserInfo(string key){
            var getUserInfo = new GetUserInfoDtq { Key = key };
            return await
               ValidateThenExecuteFaultHandledOperation<PersonInfoDto, GetUserInfoDtq>(
                   async () => await _hrService.GetUserInfo(getUserInfo), getUserInfo);
        }
        public async Task<PersonInfoDto> GetUserInfoByPersonnelNo(string key, string personnelNo){
            var getUserInfo = new GetUserInfoByPersonnelNoDtq { Key = key,PersonnalNumber = personnelNo};
            return await
               ValidateThenExecuteFaultHandledOperation<PersonInfoDto, GetUserInfoDtq>(
                   async () => await _hrService.GetUserInfo(getUserInfo), getUserInfo);
           
        }
        public async Task<BranchManagerDto> IsBranchManager(string key){
            var isManager = new IsValidBranchManagerDtq {Key = key};
            return await
               ValidateThenExecuteFaultHandledOperation<BranchManagerDto, IsValidBranchManagerDtq>(
                   async () => await _hrService.IsBranchManager(isManager), isManager);
        }
        public async Task<IEnumerable<WorkSectionDto>> GetWorkSections(){
          return  await _hrService.GetWorkSections();
        }
        public async Task<IEnumerable<BranchDto>> GetBranches()
        {
            return await _hrService.GetBranches();
        }
    }
}