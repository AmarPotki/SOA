using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.VipBanking;
using RahyabServices.DataAccess.Repositories.VipBanking.Interfaces;
namespace RahyabServices.DataAccess.Repositories.VipBanking.Implementations{
    public class VipDelinquentRepository : VipBankingRepositoryBase<VipDelinquent>, IVipDelinquentRepository
    {
        public VipDelinquentRepository(IDataContextFactory databaseFactory) : base(databaseFactory) { }
        public async Task<IEnumerable<VipDelinquent>> GetAll(int skip, int take)
        {
            return
                await
                    QueryAsync(
                        async x =>
                            await x.Where(f => f.KeyId > 0).OrderBy(o => o.KeyId).Skip(skip).Take(take).ToListAsync());
        }
        public async Task<IEnumerable<VipDelinquent>> GetDelinquents(string customerNumber, int skip, int take){
            return
                 await
                     QueryAsync(
                         async x =>
                             await x.Where(f => f.CustomerId==customerNumber).OrderBy(o => o.KeyId).Skip(skip).Take(take).ToListAsync());
        }
        public async Task<int> GetDelinquentsCount(string customerNumber){
            return await CountAsync(x => x.CustomerId == customerNumber);
        }
    }
}