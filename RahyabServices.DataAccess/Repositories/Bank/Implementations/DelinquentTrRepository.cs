using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class DelinquentTrRepository : BankRepositoryBase<DelinquentTr>, IDelinquentTrRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public DelinquentTrRepository(IDataContextFactory databaseFactory, IDataContextFactory dataContextFactory)
            : base(databaseFactory){
            _dataContextFactory = dataContextFactory;
        }
        public async Task<IEnumerable<BranchDelinquentReport>> GetDelinquentBranch(string fromDate, string toDate){
            using (var db = _dataContextFactory.GetBankDataContext()){
                var query =
                    $@"select  contract as Contract, BranchGiven as BranchCode, (sum(case when LoanReturnedStatus <> 'r' then amt else 0 end) - sum(case when LoanReturnedStatus = 'r' then amt else 0 end)) as Amount
from ( select Contract, BranchGiven, TraceCode, abs(TransAmount) amt, TransDate,  BranchTrans, ActionCode, InstructionCode, LoanReturnedStatus from AGLTRNSC where HisDate between '{fromDate}' and '{toDate}'
and ( (ActionCode = '0014' and InstructionCode in ('0005','0007')) or (ActionCode = '0015' and InstructionCode in ('0005','0010')) or (ActionCode = '0016' and InstructionCode in ('0005','0006','0007'))
or (ActionCode = '0018' and InstructionCode in ('0005','0010')) or (ActionCode = '0024' and InstructionCode in ('0005','0010')) or (ActionCode = '0065' and InstructionCode in ('0005','0010')) or 
(ActionCode = '0066' and InstructionCode in ('0005','0010')) or (ActionCode = '0079' and InstructionCode in ('0005','0010')) or (ActionCode = '0080' and InstructionCode in ('0005')) or
(ActionCode = '00B4' and InstructionCode in ('0005','0010')) or (ActionCode = '00B6' and InstructionCode in ('0005','0010')) or (ActionCode = '00B5' and InstructionCode in ('0005','0010'))
or (ActionCode = '00B7' and InstructionCode in ('0005','0010')) or (ActionCode = '00B8' and InstructionCode in ('0005','0010')) or (ActionCode = '00B9' and InstructionCode in ('0005','0010')) or 
(ActionCode = '00BA' and InstructionCode in ('0005','0010')) or (ActionCode = '00BB' and InstructionCode in ('0005','0010')) ) group by Contract,   BranchGiven, TraceCode, abs(TransAmount), 
TransDate, BranchTrans, ActionCode, InstructionCode, LoanReturnedStatus ) as a group by contract, BranchGiven";
                return
                    await
                        db.Database.SqlQuery<BranchDelinquentReport>(query)
                            .ToListAsync();
            }
        }
        public async Task<IEnumerable<BranchDelinquentReport>> GetDelinquentBranch(string fromDate, string toDate,
            string branchCode){
            using (var db = _dataContextFactory.GetBankDataContext()){
                var query =
                    $@"select  contract as Contract, BranchGiven as BranchCode, (sum(case when LoanReturnedStatus <> 'r' then amt else 0 end) - sum(case when LoanReturnedStatus = 'r' then amt else 0 end)) as Amount
from ( select Contract, BranchGiven, TraceCode, abs(TransAmount) amt, TransDate,  BranchTrans, ActionCode, InstructionCode, LoanReturnedStatus from AGLTRNSC where HisDate between '{fromDate}' and '{toDate}' and BranchGiven ='{branchCode}'
and ( (ActionCode = '0014' and InstructionCode in ('0005','0007')) or (ActionCode = '0015' and InstructionCode in ('0005','0010')) or (ActionCode = '0016' and InstructionCode in ('0005','0006','0007'))
or (ActionCode = '0018' and InstructionCode in ('0005','0010')) or (ActionCode = '0024' and InstructionCode in ('0005','0010')) or (ActionCode = '0065' and InstructionCode in ('0005','0010')) or 
(ActionCode = '0066' and InstructionCode in ('0005','0010')) or (ActionCode = '0079' and InstructionCode in ('0005','0010')) or (ActionCode = '0080' and InstructionCode in ('0005')) or
(ActionCode = '00B4' and InstructionCode in ('0005','0010')) or (ActionCode = '00B6' and InstructionCode in ('0005','0010')) or (ActionCode = '00B5' and InstructionCode in ('0005','0010'))
or (ActionCode = '00B7' and InstructionCode in ('0005','0010')) or (ActionCode = '00B8' and InstructionCode in ('0005','0010')) or (ActionCode = '00B9' and InstructionCode in ('0005','0010')) or 
(ActionCode = '00BA' and InstructionCode in ('0005','0010')) or (ActionCode = '00BB' and InstructionCode in ('0005','0010')) ) group by Contract,   BranchGiven, TraceCode, abs(TransAmount), 
TransDate, BranchTrans, ActionCode, InstructionCode, LoanReturnedStatus ) as a group by contract, BranchGiven";
                return
                    await
                        db.Database.SqlQuery<BranchDelinquentReport>(query)
                            .ToListAsync();
            }
        }
    }
}