using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class AccountInfoRepository : BankRepositoryBase<AccountInfo>, IAccountInfoRepository{
        private readonly IDataContextFactory _dataContextFactory;
        public AccountInfoRepository(IDataContextFactory databaseFactory)
            : base(databaseFactory){
            _dataContextFactory = databaseFactory;
        }
        public bool IsValid(string accountnumber){
            return Query(x => x.Any(f => f.Accountnumber == accountnumber));
        }
        public async Task<bool> IsvalidAsync(string accountnumber){
            return await QueryAsync(async x => await x.AnyAsync(f => f.Accountnumber == accountnumber));
        }
        public async Task<bool> IsvalidByBranchCodeAsync(string accountnumber, string branchCode){
            return
                await
                    QueryAsync(
                        async x =>
                            await x.AnyAsync(f => f.Accountnumber == accountnumber && f.OpenerBranchCode == branchCode));
        }
    }
}