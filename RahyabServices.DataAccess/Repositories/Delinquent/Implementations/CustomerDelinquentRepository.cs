using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Common.Convertors;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Implementations{
    public class CustomerDelinquentRepository : DelinquentRepositoryBase<CustomerDelinquent>,
        ICustomerDelinquentRepository{
        private readonly IDataContextFactory _dataContextFactory;
        private readonly IDateTimeConvertor _dateTimeConvertor;
        public CustomerDelinquentRepository(IDataContextFactory databaseFactory, IDateTimeConvertor dateTimeConvertor)
            : base(databaseFactory){
            _dataContextFactory = databaseFactory;
            _dateTimeConvertor = dateTimeConvertor;
        }
        public async Task<bool> IsExistAsync(int id){
            return await QueryAsync(async f => await f.AnyAsync(x => x.Id == id));
        }
        public async Task<bool> IsExistAsync(string customerCode){
            return await QueryAsync(async f => await f.AnyAsync(x => x.ContractCode == customerCode));
        }
        public async Task<CustomerDelinquent> GetCustomerDelinquentWithState(int CustomerDelinquentId){
            return
                await
                    QueryAsync(
                        async q =>
                            await q.Include("CurrentState").FirstOrDefaultAsync(x => x.Id == CustomerDelinquentId));
        }
        public IEnumerable<CustomerDelinquent> GetItemsNotInDate(string desireDate){
            return Query(q => q.Where(w => w.HistoryDate != desireDate)).ToList();
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetItemsNotInDateNoTrackingAsync(string desireDate,
            BankType bankType){
            return
                await
                    QueryAsync(
                        async q =>
                            await q.Where(w => w.HistoryDate != desireDate && w.BankType == bankType).ToListAsync(),
                        true);
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetExpiredEvents(DateTime date){
            return
                await
                    QueryAsync(
                        async q =>
                            await
                                q.Include("CurrentState")
                                    .Where(w => w.CurrentState.ExpireDate == date &!w.IsArchived)
                                    .ToListAsync());
        }
        public IEnumerable<CustomerDelinquent> GetContractsWithDesireRemainingTime(TimeBase timeBase){
            var dayRemain = DateTime.Now.AddDays(+1);
            var weekRemain = DateTime.Now.AddDays(+7);
            var monthRemain = DateTime.Now.AddDays(+30);
            switch (timeBase){
                case TimeBase.Day:
                    return
                        Query(q => q.Where(w => w.MaturityDate == dayRemain).ToList());
                case TimeBase.Week:
                    return
                        Query(q => q.Where(w => w.MaturityDate <= weekRemain && w.MaturityDate >= dayRemain).ToList());
                case TimeBase.Month:
                    return
                        Query(q => q.Where(w => w.MaturityDate >= monthRemain && w.MaturityDate <= weekRemain).ToList());
                default:
                    throw new NotSupportedException("Not Supported Operation");
            }
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetContractsWithDesireRemainingTimeAsync(TimeBase timeBase){
            var dayRemain = DateTime.Now.AddDays(+1);
            var weekRemain = DateTime.Now.AddDays(+7);
            var monthRemain = DateTime.Now.AddDays(+30);
            switch (timeBase){
                case TimeBase.Day:
                    return
                        await
                            QueryAsync(async q => await q.Where(w => w.MaturityDate == dayRemain).ToListAsync());
                case TimeBase.Week:
                    return
                        await
                            QueryAsync(
                                async q =>
                                    await
                                        q.Where(w => w.MaturityDate <= weekRemain && w.MaturityDate >= dayRemain)
                                            .ToListAsync());
                case TimeBase.Month:
                    return
                        await
                            QueryAsync(
                                async q =>
                                    await
                                        q.Where(w => w.MaturityDate <= monthRemain && w.MaturityDate >= weekRemain)
                                            .ToListAsync());
                default:
                    throw new NotSupportedException("Not Supported Operation");
            }
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetContractsByBranchAsync(string branchCode){
            return
                await
                    QueryAsync(async f => await f.Where(x => x.BranchCode == branchCode & !x.IsArchived).ToListAsync());
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetContractsByBranchAsync(int branchId){
            return
                await
                    QueryAsync(async f => await f.Where(x => x.BranchId == branchId && !x.IsArchived).ToListAsync());
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetAllDebtContractsByBranchAsync(int branchId)
        {
            return
                await
                    QueryAsync(async f => await f.Where(x => x.BranchId == branchId & (x.Status == "5" || x.Status == "1" || x.Status == "2") && !x.IsArchived).ToListAsync());
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetContractsByBranchAndStatusAsync(string branchCode,
            string status){
            return
                await
                    QueryAsync(
                        async f =>
                            await
                                f.Where(x => x.BranchCode == branchCode && x.Status == status && !x.IsArchived)
                                    .ToListAsync());
        }
        public async Task<decimal> GetTotalSumAsync(string branchCode){
            return
                await
                    QueryAsync(
                        async q =>
                            await
                                q.Where(f => f.BranchCode == branchCode && !f.IsArchived)
                                    .SumAsync(p => p.ApprovedAmount));
        }
        public IEnumerable<CustomerDelinquent> GetContractsByBranch(string branchCode){
            return Query(q => q.Where(x => x.BranchCode == branchCode && !x.IsArchived).ToList());
        }
        public CustomerDelinquent GetContractById(int id){
            return Query(q => q.SingleOrDefault(x => x.Id == id));
        }
        public async Task<CustomerDelinquent> GetContractByIdAsync(int id){
            return await QueryAsync(async q => await q.SingleOrDefaultAsync(x => x.Id == id));
        }
        public async Task<CustomerDelinquent> GetContractByCustomerNumberAsync(string customerNumber){
            return await QueryAsync(async q => await q.SingleOrDefaultAsync(x => x.CustomerNumber == customerNumber));
        }
        public CustomerDelinquent GetContractByCustomerNumber(string customerNumber){
            return Query(q => q.SingleOrDefault(x => x.CustomerNumber == customerNumber));
        }
        public async Task<int> GetCurrentCountAsync(string branchCode){
            return await CountAsync(f => f.BranchCode == branchCode && f.Status == "0" && !f.IsArchived);
        }
        public async Task<int> GetExpireCountAsync(string branchCode){
            return await CountAsync(f => f.BranchCode == branchCode && f.Status == "1" && !f.IsArchived);
        }
        public async Task<int> GetDueDateCountAsync(string branchCode){
            return await CountAsync(f => f.BranchCode == branchCode && f.Status == "6" && !f.IsArchived);
        }
        public async Task<int> GetTotalCountAsync(string branchCode){
            return await CountAsync(f => f.BranchCode == branchCode && !f.IsArchived);
        }
        public async Task<int> GetOneMonthToDueDateCountAsync(string branchCode){
            return
                await
                    CountAsync(
                        f =>
                            f.BranchCode == branchCode && !f.IsArchived &&
                            (f.CurrentState is FirstAnnounceState || f.CurrentState is SecondAnnounceState ||
                             f.CurrentState is ThirdAnnounceState));
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetOneMonthToDueDateAsync(string branchCode){
            return
                await
                    QueryAsync(
                        x =>
                            x.Where(
                                f =>
                                    f.BranchCode == branchCode && !f.IsArchived &&
                                    (f.CurrentState is FirstAnnounceState || f.CurrentState is SecondAnnounceState ||
                                     f.CurrentState is ThirdAnnounceState)).ToListAsync());
        }
        public async Task<decimal> GetSumOneMonthToDueDateAsync(string branchCode){
            return
                await
                    QueryAsync(
                        x =>
                            x.Where(
                                f =>
                                    f.BranchCode == branchCode && !f.IsArchived &&
                                    (f.CurrentState is FirstAnnounceState || f.CurrentState is SecondAnnounceState ||
                                     f.CurrentState is ThirdAnnounceState)).SumAsync(p => p.ApprovedAmount));
        }
        public async Task<decimal> GetCurrentSumAsync(string branch){
            return
                await
                    QueryAsync(
                        x =>
                            x.Where(f => f.BranchCode == branch && f.Status == "0" && !f.IsArchived)
                                .SumAsync(p => p.MandehJari));
        }
        public async Task<decimal> GetDueDateSumAsync(string branchCode){
            return
                await
                    QueryAsync(
                        x =>
                            x.Where(f => f.BranchCode == branchCode && f.Status == "6" && !f.IsArchived)
                                .SumAsync(p => p.Remaining));
        }
        public async Task<decimal> GetExpireSumAsync(string branchCode){
            return
                await
                    QueryAsync(
                        x =>
                            x.Where(f => f.BranchCode == branchCode && f.Status == "1" && !f.IsArchived)
                                .SumAsync(p => p.MandehGheireJary));
        }
        public async Task<int> GetBadDebtAsync(string branchCode){
            return await CountAsync(f => f.BranchCode == branchCode && f.Status == "2" && !f.IsArchived);
        }
        public async Task<decimal> GetBadDebtSumAsync(string branchCode){
            return
                await
                    QueryAsync(
                        x =>
                            x.Where(f => f.BranchCode == branchCode && f.Status == "2" && !f.IsArchived)
                                .SumAsync(p => p.MandehGheireJary));
        }
        public async Task<int> GetPostponedCountAsync(string branchCode){
            return await CountAsync(f => f.BranchCode == branchCode && f.Status == "5" && !f.IsArchived);
        }
        public async Task<decimal> GetPostponedSumAsync(string branchCode){
            return
                await
                    QueryAsync(
                        x =>
                            x.Where(f => f.BranchCode == branchCode && f.Status == "5" && !f.IsArchived)
                                .SumAsync(p => p.MandehGheireJary));
        }
        public async Task<int> GetAllDebtsCountAsync(string branchCode){
            return
                await
                    CountAsync(
                        f =>
                            f.BranchCode == branchCode && (f.Status == "5" || f.Status == "1" || f.Status == "2") &&
                            !f.IsArchived);
        }
        public async Task<decimal> GetAllDebtsSumAsync(string branchCode){
            return
                await
                    QueryAsync(
                        x =>
                            x.Where(
                                f =>
                                    f.BranchCode == branchCode &&
                                    (f.Status == "5" || f.Status == "1" || f.Status == "2") && !f.IsArchived)
                                .SumAsync(p => p.MandehGheireJary));
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetAllDebtsAsync(string branchCode){
            return
                await
                    QueryAsync(
                        async f =>
                            await
                                f.Where(
                                    x =>
                                        x.BranchCode == branchCode &&
                                        (x.Status == "5" || x.Status == "1" || x.Status == "2") && !x.IsArchived)
                                    .ToListAsync());
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetCustomerDelinquents(){
            return await QueryAsync(async q => await q.Where(x => !x.IsArchived).ToListAsync());
        }
        public async Task<string> GetMaxHisDate(BankType bankType){
            return await QueryAsync(async q => await q.Where(x => x.BankType == bankType).MaxAsync(f => f.HistoryDate));
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetAllAbisAsync(){
            return
                await
                    QueryAsync(
                        async f =>
                            await
                                f.Where(x => x.BankType == BankType.Abis)
                                    .ToListAsync(), true);
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetAllBankIranAsync(){
            return
                await
                    QueryAsync(
                        async f =>
                            await
                                f.Where(x => x.BankType == BankType.BankIran)
                                    .ToListAsync(), true);
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetAllAsync(){
            return
                       await
                           QueryAsync(
                               async f =>
                                   await
                                       f.Where(x => x.Id !=0).ToListAsync());
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetContractsByOldBranchesAndStatusAsync(IEnumerable<string> codes, string status){
            return
                   await
                       QueryAsync(
                           async f =>
                               await
                                   f.Where(x => codes.Contains(x.OldBranchCode) && x.Status == status && !x.IsArchived)
                                       .ToListAsync());
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetContractsByOldBranchesAsync(IEnumerable<string> codes){
            return
                await
                    QueryAsync(async f => await f.Where(x => codes.Contains(x.OldBranchCode) && !x.IsArchived).ToListAsync());
        }
        public async Task<int> GetTotalCountByOldBranchesAsync(IEnumerable<string> branchCodes){
            return await CountAsync(x => branchCodes.Contains(x.OldBranchCode) && !x.IsArchived);
        }
        public async Task<decimal> GetTotalSumByOldBranchesAsync(IEnumerable<string> branchCodes){
            return
                await
                    QueryAsync(
                        async q =>
                            await
                                q.Where(f => branchCodes.Contains(f.OldBranchCode) && !f.IsArchived)
                                    .SumAsync(p => p.ApprovedAmount));
        }
        public async Task<int> GetCurrentCountByOldBranchesAsync(IEnumerable<string> branchCodes){
            return await CountAsync(f => branchCodes.Contains( f.OldBranchCode) && f.Status == "0" && !f.IsArchived);
        }
        public async Task<decimal> GetCurrentSumByOldBranchesAsync(IEnumerable<string> branchCodes){
            return
              await
                  QueryAsync(
                      x =>
                          x.Where(f => branchCodes.Contains( f.OldBranchCode) && f.Status == "0" && !f.IsArchived)
                              .SumAsync(p => p.MandehJari));
        }
        public async Task<int> GetExpireCountByOldBranchesAsync(IEnumerable<string> branchCodes){
            return await CountAsync(f => branchCodes.Contains( f.OldBranchCode) && f.Status == "1" && !f.IsArchived);
        }
        public async Task<decimal> GetExpireSumByOldBranchesAsync(IEnumerable<string> branchCodes){
            return
          await
              QueryAsync(
                  x =>
                      x.Where(f => branchCodes.Contains(f.OldBranchCode) && f.Status == "1" && !f.IsArchived)
                          .SumAsync(p => p.MandehGheireJary));
        }
        public async Task<int> GetDueDateCountByOldBranchesAsync(IEnumerable<string> branchCodes){
            return await CountAsync(f => branchCodes.Contains(f.OldBranchCode) && f.Status == "6" && !f.IsArchived);
        }
        public async Task<decimal> GetDueDateSumByOldBranchesAsync(IEnumerable<string> branchCodes){
            return
           await
               QueryAsync(
                   x =>
                       x.Where(f => branchCodes.Contains(f.OldBranchCode) && f.Status == "6" && !f.IsArchived)
                           .SumAsync(p => p.Remaining));
        }
        public async Task<int> GetBadDebtByOldBranchesAsync(IEnumerable<string> branchCodes){
            return await CountAsync(f => branchCodes.Contains(f.OldBranchCode) && f.Status == "2" && !f.IsArchived);
        }
        public async Task<decimal> GetBadDebtSumByOldBranchesAsync(IEnumerable<string> branchCodes){
            return
               await
                   QueryAsync(
                       x =>
                           x.Where(f => branchCodes.Contains(f.OldBranchCode) && f.Status == "2" && !f.IsArchived)
                               .SumAsync(p => p.MandehGheireJary));
        }
        public async Task<int> GetPostponedByOldBranchesCountAsync(IEnumerable<string> branchCodes){
            return await CountAsync(f => branchCodes.Contains(f.OldBranchCode) && f.Status == "5" && !f.IsArchived);
        }
        public async Task<decimal> GetPostponedByOldBranchesSumAsync(IEnumerable<string> branchCodes){
            return
               await
                   QueryAsync(
                       x =>
                           x.Where(f => branchCodes.Contains(f.OldBranchCode) && f.Status == "5" && !f.IsArchived)
                               .SumAsync(p => p.MandehGheireJary));
        }
        public async Task<int> GetOneMonthToDueDateByOldBranchesCountAsync(IEnumerable<string> branchCodes){
            return
               await
                   CountAsync(
                       f =>
                           branchCodes.Contains(f.OldBranchCode) && !f.IsArchived &&
                           (f.CurrentState is FirstAnnounceState || f.CurrentState is SecondAnnounceState ||
                            f.CurrentState is ThirdAnnounceState));
        }
        public async Task<decimal> GetSumOneMonthToDueDateByOldBranchesAsync(IEnumerable<string> branchCodes){
            return
               await
                   QueryAsync(
                       x =>
                           x.Where(
                               f =>
                                   branchCodes.Contains( f.OldBranchCode) && !f.IsArchived &&
                                   (f.CurrentState is FirstAnnounceState || f.CurrentState is SecondAnnounceState ||
                                    f.CurrentState is ThirdAnnounceState)).SumAsync(p => p.ApprovedAmount));
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetOneMonthToDueDateByBranchesAsync(IEnumerable<string> branchCodes){
            return
               await
                   QueryAsync(
                       x =>
                           x.Where(
                               f =>
                                   branchCodes.Contains(f.OldBranchCode ) && !f.IsArchived &&
                                   (f.CurrentState is FirstAnnounceState || f.CurrentState is SecondAnnounceState ||
                                    f.CurrentState is ThirdAnnounceState)).ToListAsync());
        }
        public async Task<IEnumerable<CustomerDelinquent>> GetAllDebtsByOldBranchesAsync(IEnumerable<string> branchCodes){
            return
                await
                    QueryAsync(
                        async f =>
                            await
                                f.Where(
                                    x =>
                                       branchCodes.Contains(x.OldBranchCode) &&
                                        (x.Status == "5" || x.Status == "1" || x.Status == "2") && !x.IsArchived)
                                    .ToListAsync());
        }
        public async Task<int> GetAllDebtsByOldBranchCountAsync(IEnumerable<string> branchCodes){
            return
                     await
                         CountAsync(
                             f =>
                                  branchCodes.Contains(f.OldBranchCode) && (f.Status == "5" || f.Status == "1" || f.Status == "2") &&
                                 !f.IsArchived);
        }
        public async Task<decimal> GetAllDebtsByOldBranchSumAsync(IEnumerable<string> branchCodes){
            return
           await
               QueryAsync(
                   x =>
                       x.Where(
                           f =>
                               branchCodes.Contains(f.OldBranchCode) &&
                               (f.Status == "5" || f.Status == "1" || f.Status == "2") && !f.IsArchived)
                           .SumAsync(p => p.MandehGheireJary));
        }
    }
}