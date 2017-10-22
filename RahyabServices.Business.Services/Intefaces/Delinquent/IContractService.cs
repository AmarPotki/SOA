using System.Collections.Generic;
using System.Threading.Tasks;

using RahyabServices.Business.Dtos.Delinquent.Contracts;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
namespace RahyabServices.Business.Services.Intefaces.Delinquent
{
    public interface IContractService
    {
        #region BranchRequest

        Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsAsync(GetContractsByUserNameDto getContractsByUserNameDto);
        IEnumerable<CustomerDelinquentDto> GetBranchContracts(GetContractsByUserNameDto getContractsByUserNameDto);
        Task<CountAndSumDto> GetBranchContractsCountAsync(GetContractsCountDto getContractsCountDto);
        Task<CountAndSumDto> GetCurrentContractsCountAsync(GetContractsCountDto contractsCountDto);
        Task<CountAndSumDto> GetExpireContractsCountAsync(GetContractsCountDto contractsCountDto);
        Task<CountAndSumDto> GetDueDateContractsCountAsync(GetContractsCountDto contractsCountDto);
        Task<CountAndSumDto> GetBadDebtContractsCountAsync(GetContractsCountDto contractsCountDto);
        Task<CountAndSumDto> GetPostponedContractsCountAsync(GetContractsCountDto contractsCountDto);
        Task<CountAndSumDto> GetAllDebtsCountAsync(GetContractsCountDto allDebtsCountDto);

        Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsAsync(GetPostponedDto postponedDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsAsync(GetBadDebtDto getBadDebtDto);
       
        Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsAsync(
            GetOneMonthToDueDateContractsDto getOneMonthToDueDateContractsDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsAsync(GetExpireContractsDto expireContractsDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsAsync(GetDueDateContractsDto getDueDateContractsDto);
        Task<CountAndSumDto> GetOneMonthToDueDateCountAsync(GetContractsCountDto contractsCountDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsAsync(GetCurrentContractsDto getCurrentContractsDto);

        #endregion

        #region ManagerRequest

        Task<IEnumerable<AllBranchActivityDto>> GetAllBranchActivityAsync(
            GetAllBranchActivityDtq getAllBranchActivityDtq);
        Task<string> GetLastAbisUpdateDateAsync(GetLastUpdateDateDto getLastUpdateDateDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetBranchContractsByBranchCodeAsync(GetContractsByBranchCodeDto getContractsByBranchCodeDto);
        IEnumerable<CustomerDelinquentDto> GetBranchContractsByBranchCode(GetContractsByBranchCodeDto getContractsByBranchCodeDto);
        Task<CountAndSumDto> GetBranchContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto getContractsCountByBranchCodeDto);
        Task<CountAndSumDto> GetCurrentContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto);
        Task<CountAndSumDto> GetExpireContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto);
        Task<CountAndSumDto> GetDueDateContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto);
        Task<CountAndSumDto> GetBadDebtContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto);
        Task<CountAndSumDto> GetPostponedContractsCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto);
        Task<CountAndSumDto> GetAllDebtsCountByBranchCodeAsync(GetAllDebtsCountByBranchCodeDto getAllDebtsCountByBranchCodeDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsByBranchCodeAsync(GetAllDebtsContractsByBranchCodeDto getAllDebtsContractsByBranchCodeDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetAllDebtsContractsAsync(GetAllDebtsContractsDto allDebtsContractsDto);

        
        Task<IEnumerable<CustomerDelinquentDto>> GetPostponedContractsByBranchCodeAsync(GetPostponedByBranchCodeDto postponedByBranchCodeDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetBadDebtContractsByBranchCodeAsync(GetBadDebtByBranchCodeDto getBadDebtByBranchCodeDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetOneMonthToDueDateContractsByBranchCodeAsync(
            GetOneMonthToDueDateContractsByBranchCodeDto getOneMonthToDueDateContractsByBranchCodeDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetExpireContractsByBranchCodeAsync(GetExpireContractsByBranchCodeDto expireContractsByBranchCodeDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetDueDateContractsByBranchCodeAsync(GetDueDateContractsByBranchCodeDto getDueDateContractsByBranchCodeDto);
        Task<CountAndSumDto> GetOneMonthToDueDateCountByBranchCodeAsync(GetContractsCountByBranchCodeDto contractsCountByBranchCodeDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetCurrentContractsByBranchCodeAsync(GetCurrentContractsByBranchCodeDto getCurrentContractsByBranchCodeDto);
        #endregion

        Task<string> GetLastBankIranUpdateDateAsync(GetLastUpdateDateDto getLastUpdateDateDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryAsync(GetCustomerDelinquentHistoryDto customerDelinquentHistoryDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetCustomerDelinquentHistoryByBranchCodeAsync(GetCustomerDelinquentHistoryByBranchCodeDto customerDelinquentHistoryByBranchCodeDto);
        Task<IEnumerable<CustomerDelinquentDto>> GetAllCustomerDelinquentsHistoryAsync(
            GetAllCustomerDelinquentsHistoryDto allCustomerDelinquentsHistoryDto);

    }
}