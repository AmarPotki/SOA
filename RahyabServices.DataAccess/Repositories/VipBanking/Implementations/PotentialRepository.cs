using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.VipBanking;
using RahyabServices.DataAccess.Repositories.VipBanking.Interfaces;
namespace RahyabServices.DataAccess.Repositories.VipBanking.Implementations{
    public class PotentialRepository : VipBankingRepositoryBase<Potential>, IPotentialRepository
    {

        public PotentialRepository(IDataContextFactory databaseFactory) : base(databaseFactory)
        {
        }

        public async Task<IEnumerable<Potential>> GetAll(int skip, int take)
        {
            return await QueryAsync(async x => await x.Where(f => f.KeyId > 0).OrderBy(o => o.KeyId).Skip(skip).Take(take).ToListAsync());
        }

        public async Task<Potential> GetPotentialByCustomerNumber(string customerNumber){
            return await QueryAsync(async x => await x.FirstOrDefaultAsync(f => f.CustomerID == customerNumber));
        }
        public async Task<IEnumerable<Potential>> GetFiltered(int skip, int take, string filter)
        {
            var inputCode = filter.Split(':')[1];
            return await QueryAsync(async x => await x.Where(f => f.KeyId > 0 && f.InputCode == inputCode).OrderBy(o => o.KeyId).ToListAsync());
        }
    }
}