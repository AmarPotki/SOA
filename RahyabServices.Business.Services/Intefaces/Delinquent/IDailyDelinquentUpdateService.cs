using System;
using System.Threading.Tasks;

namespace RahyabServices.Business.Services.Intefaces.Delinquent
{
    public interface IDailyDelinquentUpdateService
    {
        Task<bool> UpdateWorkingCopyOfDatabaseAsync();
        Task<bool> UpdateWorkingCopyOfAbisDatabaseAsync(DateTime date);
        Task<bool> UpdateWorkingCopyOfBankIranDatabaseAsync(DateTime date);
        Task<bool> UpdateWorkingCopyOfGuaranteeDatabaseAsync(DateTime date);
        Task<bool> UpdateBranchClaim();
    }
}