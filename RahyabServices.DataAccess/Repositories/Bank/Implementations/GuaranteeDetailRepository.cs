

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Bank;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Bank.Implementations{
    public class GuaranteeDetailRepository : BankRepositoryBase<GuaranteeDetail>, IGuaranteeDetailRepository{
        public GuaranteeDetailRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
        public async Task<IEnumerable<GuaranteeDetail>> GetByPersianDate(string persianDate){
            return await QueryAsync(async f =>await f.Where(x => x.HisDate == persianDate).ToListAsync());
        }
    }
}