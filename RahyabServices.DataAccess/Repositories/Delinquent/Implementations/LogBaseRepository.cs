using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Implementations
{
    public class LogBaseRepository : DelinquentRepositoryBase<LogBase>, ILogBaseRepository{
        private readonly IDataContextFactory _databaseFactory;
        public LogBaseRepository(IDataContextFactory databaseFactory)
            : base(databaseFactory){
                _databaseFactory = databaseFactory;
        }
        public IEnumerable<LogBase> GetLogs(long CustomerDelinquentId)
        {
            return Query(f => f.Where(x => x.CustomerDelinquentId == CustomerDelinquentId).ToList());
        }

        public async Task<IEnumerable<AllBranchActivityDto>> GetAllBranchActivity(DateTime fromDate, DateTime toDate){
            var strFrom = fromDate.ToString("yyyy-MM-dd");
            var strTo = toDate.ToString("yyyy-MM-dd");
            using (var db = _databaseFactory.GetRahyabServicesDataContext()){
                var quyery = string.Format(
                    @"SELECT cd.BranchName , cd.BranchCode , logb.Discriminator as Type , Count(cd.BranchName) as Total
  FROM [RahyabServices].[dbo].[CustomerDelinquents] as cd join
   [RahyabServices].[dbo].[LogBases] as logb  on (cd.Id = logb.CustomerDelinquent_Id)
   where logb.Author !='System' and logb.Author !='spfarm' and logb.Created >= CONVERT(datetime,'{0}') and logb.Created <= CONVERT(datetime,'{1}')
   group by cd.BranchName , cd.BranchCode , logb.Discriminator
      order by cd.BranchCode", strFrom, strTo);
                return await db.Database.SqlQuery<AllBranchActivityDto>(quyery).ToListAsync();
            }
        }
        public async Task<IEnumerable<LogBase>> GetLogsAsync(long CustomerDelinquentId)
        {
            return
                await
                    QueryAsync(
                        async f => await f.Where(x => x.CustomerDelinquentId == CustomerDelinquentId).ToListAsync());
        }

        public async Task<IEnumerable<SmsLog>> GetPendingSmsesAsync()
        {
            return
                await
                    QueryAsync(
                        async f => await f.OfType<SmsLog>().Where(x => x.IsDelivered == false).ToListAsync());
        }
        public async Task<bool> IsExistRequestSplitLog(int CustomerDelinquentId)
        {
            return
                await
                    QueryAsync(
                        async f => await f.OfType<RequestSplitLog>().AnyAsync(x => x.IsApprove == null && x.CustomerDelinquentId == CustomerDelinquentId));
        }
        public async Task<bool> IsExistAllowEditRequestSplitLog(int id,int CustomerDelinquentId)
        {
            return
                await
                    QueryAsync(
                        async f => await f.OfType<RequestSplitLog>().AnyAsync(x => x.AllowEdit && x.Id==id && x.CustomerDelinquentId == CustomerDelinquentId));
        }
        public  async Task<bool> IsExistRequestGivinAChanceLog(int CustomerDelinquentId){
            return
                await
                    QueryAsync(
                        async f => await f.OfType<RequestGivingAChanceLog>().AnyAsync(x => x.IsApprove  == null && x.CustomerDelinquentId == CustomerDelinquentId));
        }
        public async Task<bool> IsExistRequestClearingLog(int CustomerDelinquentId)
        {
            return
                await
                    QueryAsync(
                        async f => await f.OfType<RequestClearingLog>().AnyAsync(x => x.IsApprove == null && x.CustomerDelinquentId == CustomerDelinquentId));
        }
        public async Task<bool> IsExistClearingLog(int id)
        {
            return
                await
                    QueryAsync(
                        async f => await f.OfType<ClearingLog>().AnyAsync(x => x.AllowEdit && x.Id == id));
        }
        public async Task<RequestSplitLog> GetRequestSplitLog(int CustomerDelinquentId)
        {
            return
                await
                    QueryAsync(
                        async f => await f.OfType<RequestSplitLog>().FirstOrDefaultAsync(x => x.IsApprove == null && x.CustomerDelinquentId == CustomerDelinquentId));
        }

        public async Task<SplitLog> GetSplitLog(int CustomerDelinquentId)
        {
            return
                await
                    QueryAsync(
                        async f => await f.OfType<SplitLog>().FirstOrDefaultAsync(x => x.Id == CustomerDelinquentId));
        }

        public async Task<RequestGivingAChanceLog> GetRequestGivinAChanceLog(int CustomerDelinquentId)
        {
            return
                await
                    QueryAsync(
                        async f => await f.OfType<RequestGivingAChanceLog>().FirstOrDefaultAsync(x => x.IsApprove == null && x.CustomerDelinquentId == CustomerDelinquentId));
        }
        public async Task<IEnumerable<SmsLog>> GetPendingSmsesAsync(TimeSpan timeSpan)
        {
            var targetTime = DateTime.Now.Subtract(timeSpan);
            return
                await
                    QueryAsync(
                        async f => await f.OfType<SmsLog>().Where(x => x.IsDelivered == false && x.Created >= targetTime).ToListAsync());
        }
        public async Task<RequestImpunityForCrimesLog> GetRequestImpunityForCrimesLog(int CustomerDelinquentId){
            return
                 await
                     QueryAsync(
                         async f => await f.OfType<RequestImpunityForCrimesLog>().FirstOrDefaultAsync(x => x.IsApprove == null && x.CustomerDelinquentId == CustomerDelinquentId));
        }
        public async Task<RequestClearingLog> GetRequestClearingLog(int CustomerDelinquentId){
            return
                await
                    QueryAsync(
                        async f => await f.OfType<RequestClearingLog>().FirstOrDefaultAsync(x => x.IsApprove == null && x.CustomerDelinquentId == CustomerDelinquentId));
        }
        public async Task<bool> IsExistAllowEditRequestClearingLog(int id){
            return
                  await
                      QueryAsync(
                          async f => await f.OfType<RequestClearingLog>().AnyAsync(x => x.AllowEdit && x.Id == id));
        }
        public async Task<bool> IsExistRequestLogNotRespond(int CustomerDelinquentId)
        {
            if (await
                QueryAsync(
                    async f =>
                        await
                            f.OfType<RequestClearingLog>()
                                .AnyAsync(x => x.IsApprove == null && x.CustomerDelinquentId == CustomerDelinquentId))) return true;
            if (await
               QueryAsync(
                   async f =>
                       await
                           f.OfType<RequestSplitLog>()
                               .AnyAsync(x => x.IsApprove == null && x.CustomerDelinquentId == CustomerDelinquentId))) return true;
            if (await
             QueryAsync(
                 async f =>
                     await
                         f.OfType<RequestGivingAChanceLog>()
                             .AnyAsync(x => x.IsApprove == null && x.CustomerDelinquentId == CustomerDelinquentId))) return true;
            return await
                QueryAsync(
                    async f =>
                        await
                            f.OfType<RequestImpunityForCrimesLog>()
                                .AnyAsync(x => x.IsApprove == null && x.CustomerDelinquentId == CustomerDelinquentId));
        }
        
        public async Task<IEnumerable<LogBase>> GetYesterDayActions(string branchCode, DateTime date){
            return
                await
                    QueryAsync(
                        f =>
                            f.Include(t => t.CustomerDelinquent)
                                .Where(x => x.CustomerDelinquent.BranchCode == branchCode && x.Author == "System" && x.Created == date)
                                .ToListAsync());
        }
        public async Task<IEnumerable<LogTypeCustomerDelinquent>> GetLastLogAsync(string[] contractCodes, DateTime geoDate){
            using (var db = _databaseFactory.GetRahyabServicesDataContext()){
                var cc = contractCodes.Aggregate("", (current, contractCode) => current + ("," + contractCode)).Remove(0,1);

              var logs = db.Database.SqlQuery<LogTypeCustomerDelinquent>(string.Format(@"SELECT  c.ContractCode, p.Discriminator
FROM   [RahyabServices].[dbo].[CustomerDelinquents] c INNER JOIN
        (
            SELECT  CustomerDelinquent_Id,
                    MAX(Created) MaxDate
            FROM     [RahyabServices].[dbo].[LogBases]
			where Created <= '{0}'
            GROUP BY CustomerDelinquent_Id
        ) MaxDates ON c.id = MaxDates.CustomerDelinquent_Id INNER JOIN
         [RahyabServices].[dbo].[LogBases] p ON   MaxDates.CustomerDelinquent_Id = p.CustomerDelinquent_Id
                    AND MaxDates.MaxDate = p.Created and c.ContractCode in({1})", geoDate.ToString("yyyy-MM-dd"), cc));
                return await logs.ToListAsync();
            }
            
        }
    }
}