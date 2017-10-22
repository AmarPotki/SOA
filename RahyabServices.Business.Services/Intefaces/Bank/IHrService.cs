using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.BankPerson;
using RahyabServices.Business.Dtos.Delinquent.Branch;
using RahyabServices.Business.Dtos.Supplies;
namespace RahyabServices.Business.Services.Intefaces.Bank{
    public interface IHrService{
        Task<PersonInfoDto> GetUserInfo(GetUserInfoDtq getUserInfoDtq);
        Task<BranchManagerDto> IsBranchManager(IsValidBranchManagerDtq isManager);
        Task<IEnumerable<WorkSectionDto>> GetWorkSections();
        Task<IEnumerable<BranchDto>> GetBranches();
        Task<PersonInfoDto> GetUserInfo(GetUserInfoByPersonnelNoDtq getUserInfo);
    }
}