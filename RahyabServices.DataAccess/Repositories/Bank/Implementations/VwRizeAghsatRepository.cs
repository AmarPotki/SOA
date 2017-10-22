using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class VwRizeAghsatRepository :  IVwRizeAghsatRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public VwRizeAghsatRepository(IDataContextFactory databaseFactory, IDataContextFactory dataContextFactory){
            _dataContextFactory = dataContextFactory;
        }
        public async Task<IEnumerable<BranchDelinquentReport>> GetDelinquentBranch(string fromDate, string toDate)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                var query =
                    $@"SELECT  [ACCOUNTNUMBER] as Contract
      ,[AMOUNT] as Amount
      ,[NEW] as BranchCode
  FROM [AbisLoanMapping].[dbo].[vwRizeaghsat]
  where [DDATE] between '{fromDate}' and '{toDate}'";
                return
                    await
                        db.Database.SqlQuery<BranchDelinquentReport>(query)
                            .ToListAsync();
            }
        }
        public async Task<IEnumerable<BranchDelinquentReport>> GetDelinquentBranch(string fromDate, string toDate,
            string branchCode)
        {
            using (var db = _dataContextFactory.GetAbisLoanDataContext())
            {
                var query =
                    $@"SELECT  [ACCOUNTNUMBER] as Contract
      ,[AMOUNT] as Amount
      ,[NEW] as BranchCode
  FROM [AbisLoanMapping].[dbo].[vwRizeaghsat]
  where [DDATE] between '{fromDate}' and '{toDate}' and [NEW] ='{branchCode}'";
                return
                    await
                        db.Database.SqlQuery<BranchDelinquentReport>(query)
                            .ToListAsync();
            }
        }
    }
}