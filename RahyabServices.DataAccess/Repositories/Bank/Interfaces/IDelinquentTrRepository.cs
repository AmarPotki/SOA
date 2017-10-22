using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core.Bank;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces{
    public interface IDelinquentTrRepository : IBankRepository<DelinquentTr>{

        Task<IEnumerable<BranchDelinquentReport>> GetDelinquentBranch(string fromDate,string toDate);
        Task<IEnumerable<BranchDelinquentReport>> GetDelinquentBranch(string fromDate,string toDate,string branchCode);
    }
}