using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class RptftBondDelinquentRepository : BankRepositoryBase<RptftBondDelinquent>, IRptftBondDelinquentRepository{
        public RptftBondDelinquentRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
        public async Task<IEnumerable<RptftBondDelinquent>> GetBondDelinquent(string contractCode){
            return await QueryAsync(async f => await f.Where(x => x.Contract == contractCode).ToListAsync());
        }
    }
}