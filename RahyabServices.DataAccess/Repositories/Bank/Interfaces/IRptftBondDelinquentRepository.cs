using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core.Bank;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces{
    public interface IRptftBondDelinquentRepository : IBankRepository<RptftBondDelinquent>{
        Task<IEnumerable<RptftBondDelinquent>> GetBondDelinquent(string contractCode);
    }
}