using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.DataAccess.Core.Bank;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces
{
    public interface IRPTFTRepository : IBankRepository<RPTFT>{
        Task<IEnumerable<RptftAbisDetail>> GetAbisTodayCustomerDelinquentUpdatedItemsAsync(string desireDate);
        Task<IEnumerable<RptftAbisDetail>> GetAbisCustomerDelinquentAsync(string dateWithOutSlash);
        Task<IEnumerable<RptftBankIranDetail>> GetBankIranTodayCustomerDelinquentUpdatedItemsAsync(string desireDate);
        Task<IEnumerable<RptftBankIranDetail>> GetBankIranCustomerDelinquentByBranchAsync(string desireDate,
            string branchCode);
        Task<IEnumerable<RptftBankIranDetail>> GetBankIranCustomerDelinquentAsync(string dateWithOutSlash);
        Task<RptftGuarantee[]> GetGuaranteeTodayCustomerDelinquentUpdatedItemsAsync(string desireDate);
        Task<RPTFT> GetItemByContractCodeAndDateAsync(string contractCode, string desireDate);
        Task<string> GetAbisMaxHisDate();
        Task<string> GetBankIranMaxHisDate();
        Task<IEnumerable<RptftAbisDetail>> GetAbisCustomerDelinquentByBranchAsync(string dateWithOutSlash, string branchCode);
    }
}
