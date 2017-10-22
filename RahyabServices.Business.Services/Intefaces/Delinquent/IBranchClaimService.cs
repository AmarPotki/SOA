using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Dtos.Delinquent.BranchClaim;
namespace RahyabServices.Business.Services.Intefaces.Delinquent{
    public interface IBranchClaimService{
        Task<BranchClaimDto> GetBranchClaim(GetBranchClaimDto branchClaimDto);
        Task<IEnumerable<BranchClaimDto>> GetLastSevenDaysBranchClaims(GetLastSevenDaysBranchClaimsDto branchClaimsDto);
    }
}