using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Branch;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
namespace RahyabServices.Business.Services.Intefaces.Delinquent{
    public interface IBranchService{
        Task<BranchDto> GetBranchInformationAsync(GetBranchDto getBranchDto);
        Task<IEnumerable<BranchDto>> GetAllBranchesAsync(GetAllBranchDto allBranchDto);
    }
}