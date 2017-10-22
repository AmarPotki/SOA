using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces{
    public interface IVwRizeAghsatRepository{
        Task<IEnumerable<BranchDelinquentReport>> GetDelinquentBranch(string fromDate, string toDate);
        Task<IEnumerable<BranchDelinquentReport>> GetDelinquentBranch(string fromDate, string toDate,
            string branchCode);
    }
}