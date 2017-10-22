using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.DataAccess.Core.Delinquent;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Interfaces{
    public interface IBranchRepository : IDelinquentRepository<Branch>{
        Task<IEnumerable<Branch>> GetAllAsNoTracking();
        Task<Branch> GetBranchByCode(string branchCode);
        Task<Branch> GetBranchByCodeAsNoTracking(string branchCode);
        Task<IEnumerable<Branch>> GetMergBranchChildren(int parentId);
        Task<IEnumerable<Branch>> GetBranchChildrenForBranchLevel(int parentId);
    }
}