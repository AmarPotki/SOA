using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using RahyabServices.DataAccess.Core.Delinquent;

namespace RahyabServices.DataAccess.Repositories.Delinquent.Interfaces
{
    public interface ILogBaseRepository : IDelinquentRepository<LogBase>{
        Task<IEnumerable<AllBranchActivityDto>> GetAllBranchActivity(DateTime fromDate, DateTime toDate);
        IEnumerable<LogBase> GetLogs(long CustomerDelinquentId);
        Task<IEnumerable<LogBase>> GetLogsAsync(long CustomerDelinquentId);
        Task<IEnumerable<SmsLog>> GetPendingSmsesAsync();
        Task<bool> IsExistRequestSplitLog(int CustomerDelinquentId);
        Task<bool> IsExistAllowEditRequestSplitLog(int id, int CustomerDelinquentId);
        Task<bool> IsExistRequestGivinAChanceLog(int CustomerDelinquentId);
        Task<bool> IsExistRequestClearingLog(int CustomerDelinquentId);
        Task<bool> IsExistClearingLog(int id);
        Task<RequestSplitLog> GetRequestSplitLog(int CustomerDelinquentId);

        Task<SplitLog> GetSplitLog(int CustomerDelinquentId);
        Task<RequestGivingAChanceLog> GetRequestGivinAChanceLog(int CustomerDelinquentId);
        Task<IEnumerable<SmsLog>> GetPendingSmsesAsync(TimeSpan timeSpan);
        Task<RequestImpunityForCrimesLog> GetRequestImpunityForCrimesLog(int CustomerDelinquentId);
        Task<RequestClearingLog> GetRequestClearingLog(int CustomerDelinquentId);
        Task<bool> IsExistAllowEditRequestClearingLog(int id);
        Task<bool> IsExistRequestLogNotRespond(int CustomerDelinquentId);
        Task<IEnumerable<LogBase>> GetYesterDayActions(string branchCode, DateTime date);
        Task<IEnumerable<LogTypeCustomerDelinquent>> GetLastLogAsync(string[] CustomerDelinquentId, DateTime geoDate);
    }
}