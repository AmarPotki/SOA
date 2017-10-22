using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class RptftBondRepository : BankRepositoryBase<RptftBond>, IRptftBondRepository{
        public RptftBondRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
        public async Task<RptftBond> GetBond(int collatNo){
            return
                await
                    QueryAsync(
                        async f =>
                            await
                                f.OrderByDescending(d => d.TarikhSabtTazmin)
                                    .FirstOrDefaultAsync(x => x.CollatNo == collatNo));
        }
    }
}