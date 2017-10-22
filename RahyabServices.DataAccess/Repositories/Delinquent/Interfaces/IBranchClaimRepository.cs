using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.DataAccess.Core.Delinquent;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Interfaces{
    public interface IBranchClaimRepository : IDelinquentRepository<BranchClaim>{
        Task<BranchClaim> GetBranchClaim(int branchId, DateTime date);
        Task<IEnumerable<BranchClaim>> GetDaysBranchClaims(int branchId, DateTime date);
    }
}