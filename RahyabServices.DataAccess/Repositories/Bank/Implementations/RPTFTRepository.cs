using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;

namespace RahyabServices.DataAccess.Repositories.Bank.Implementations
{
    public class RPTFTRepository : BankRepositoryBase<RPTFT>, IRPTFTRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        public RPTFTRepository(IDataContextFactory databaseFactory)
            : base(databaseFactory)
        {
            _dataContextFactory = databaseFactory;
        }

        public async Task<IEnumerable<RptftAbisDetail>> GetAbisTodayCustomerDelinquentUpdatedItemsAsync(string desireDate)
        {
            using (var db = _dataContextFactory.GetBankDataContext())
            {
               return await (from s in db.RPTFTS
                            join c in db.AbisDetails on s.ContractCode equals c.ContractCode
                            where s.HistoryDate == desireDate && s.ResourceId !=null && c.HisDate ==desireDate
                            select new RptftAbisDetail
                            {
                               AbisDetail = c,
                               Rptft = s,
                            }).ToListAsync();
           
            }
         
           
        }
        public async Task<IEnumerable<RptftAbisDetail>> GetAbisCustomerDelinquentByBranchAsync(string dateWithOutSlash, string branchCode)
        {
            using (var db = _dataContextFactory.GetBankDataContext())
            {
                return await(from s in db.RPTFTS
                             join c in db.AbisDetails on s.ContractCode equals c.ContractCode
                             where s.HistoryDate == dateWithOutSlash && s.ResourceId != null && s.BranchCode == branchCode && c.BranchCode==branchCode && c.HisDate == dateWithOutSlash
                             select new RptftAbisDetail
                             {
                                 AbisDetail = c,
                                 Rptft = s,
                             }).ToListAsync();

            }
        }
        public async Task<IEnumerable<RptftAbisDetail>> GetAbisCustomerDelinquentAsync(string dateWithOutSlash)
        {
            using (var db = _dataContextFactory.GetBankDataContext())
            {
                return await (from s in db.RPTFTS
                              join c in db.AbisDetails on s.ContractCode equals c.ContractCode
                              where s.HistoryDate == dateWithOutSlash && s.ResourceId != null  && c.HisDate == dateWithOutSlash
                              select new RptftAbisDetail
                              {
                                  AbisDetail = c,
                                  Rptft = s,
                              }).ToListAsync();

            }
        }
        public async Task<IEnumerable<RptftBankIranDetail>> GetBankIranTodayCustomerDelinquentUpdatedItemsAsync(string desireDate)
        {
           
                using (var db = _dataContextFactory.GetBankDataContext())
                {
                    return await (from s in db.RPTFTS
                                  join c in db.BankIranDetails on s.ContractCode equals c.ContractCode

                                  where s.HistoryDate == desireDate && s.ResourceId == null && c.HisDate == desireDate
                                  select new RptftBankIranDetail
                                  {
                                      BankIranDetail = c,
                                      Rptft = s,
                                  }).ToListAsync();

                }
           
        }
        public async Task<IEnumerable<RptftBankIranDetail>> GetBankIranCustomerDelinquentByBranchAsync(string desireDate,string branchCode)
        {
                using (var db = _dataContextFactory.GetBankDataContext())
                {
                    return await (from s in db.RPTFTS
                                  join c in db.BankIranDetails on s.ContractCode equals c.ContractCode
                                  where s.HistoryDate == desireDate && s.ResourceId == null && s.BranchCode==branchCode && c.BranchCode==branchCode  && c.HisDate == desireDate
                                  select new RptftBankIranDetail
                                  {
                                      BankIranDetail = c,
                                      Rptft = s,
                                  }).ToListAsync();
                }
        }
        public async Task<IEnumerable<RptftBankIranDetail>> GetBankIranCustomerDelinquentAsync(string desireDate)
        {
            using (var db = _dataContextFactory.GetBankDataContext())
            {
                return await (from s in db.RPTFTS
                              join c in db.BankIranDetails on s.ContractCode equals c.ContractCode
                              where s.HistoryDate == desireDate && s.ResourceId == null  && c.HisDate == desireDate
                              select new RptftBankIranDetail
                              {
                                  BankIranDetail = c,
                                  Rptft = s,
                              }).ToListAsync();
            }
        }
        public async Task<RptftGuarantee[]> GetGuaranteeTodayCustomerDelinquentUpdatedItemsAsync(string desireDate)
        {
            using (var db = _dataContextFactory.GetBankDataContext())
            {
                return await (from s in db.RPTFTS
                              join c in db.Guarantees on s.ContractCode equals c.ContractCode

                              where s.HistoryDate == desireDate && s.ResourceId == null && c.HisDate == desireDate
                              select new RptftGuarantee
                              {
                                  ContractCode = c.ContractCode,
                                 GuaranteeStatus = c.StatusDesc,
                                 Guarantee = c,
                                 Rptft = s,
                              }).ToArrayAsync();

            }
        }
        public async Task<RPTFT> GetItemByContractCodeAndDateAsync(string contractCode, string desireDate)
        {
            return
                await
                    QueryAsync(async q => await q.SingleOrDefaultAsync(w => w.ContractCode == contractCode && w.HistoryDate == desireDate));
        }
        public async Task<string> GetAbisMaxHisDate(){
            return
                  await
                      QueryAsync(async q => await q.Where(w => w.ResourceId !=null ).MaxAsync(f=>f.HistoryDate));
        }
        public async Task<string> GetBankIranMaxHisDate(){
            return
                 await
                     QueryAsync(async q => await q.Where(w => w.ResourceId == null).MaxAsync(f => f.HistoryDate));
        }
       
    }
}
