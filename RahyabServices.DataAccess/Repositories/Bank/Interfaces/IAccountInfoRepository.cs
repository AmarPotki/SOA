using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core.Bank;

namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces
{
    public interface IAccountInfoRepository : IBankRepository<AccountInfo>{
        bool IsValid(string accountnumber);
        Task<bool> IsvalidAsync(string accountnumber);
        Task<bool> IsvalidByBranchCodeAsync(string accountnumber, string branchCode);
    }
}
