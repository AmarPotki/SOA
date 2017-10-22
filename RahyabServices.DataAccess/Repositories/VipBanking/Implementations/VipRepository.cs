using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.VipBanking;
using RahyabServices.DataAccess.Repositories.VipBanking.Interfaces;

namespace RahyabServices.DataAccess.Repositories.VipBanking.Implementations
{
    public class VipRepository : VipBankingRepositoryBase<Vip>, IVipRepository
    {
        public VipRepository(IDataContextFactory databaseFactory) : base(databaseFactory)
        {
        }

        public async Task<IEnumerable<Vip>> GetAll(int skip, int take)
        {
            return await QueryAsync(async x => await x.Where(f => f.KeyId > 0).OrderBy(o=>o.KeyId).Skip(skip).Take(take).ToListAsync());
        }
        public async Task<Vip> GetVipByCustomerNumber(string customerNumber){
            return await QueryAsync(async f => await f.FirstOrDefaultAsync(x => x.CustomerID == customerNumber));
        }

        public async Task<IEnumerable<Vip>> GetFiltered(int skip, int take,string filter){

            var custLevel = filter.Split(':')[1];

            return await QueryAsync(async x => await x.Where(f => f.KeyId > 0 && f.PrivateCustomerLevel == custLevel).OrderBy(o => o.KeyId).ToListAsync());
        }
    }
}