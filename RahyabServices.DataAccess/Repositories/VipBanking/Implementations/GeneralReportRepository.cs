using System.Data.Entity;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.VipBanking;
using RahyabServices.DataAccess.Repositories.VipBanking.Interfaces;
namespace RahyabServices.DataAccess.Repositories.VipBanking.Implementations{
    public class GeneralReportRepository : VipBankingRepositoryBase<GeneralReport>, IGeneralReportRepository{
        public GeneralReportRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
        public async Task<GeneralReport> GetMax(){
            var max = await QueryAsync(async f => await f.MaxAsync(x => x.KeyId));
            return await QueryAsync(async f => await f.FirstOrDefaultAsync(x => x.KeyId == max));
        }
    }
}