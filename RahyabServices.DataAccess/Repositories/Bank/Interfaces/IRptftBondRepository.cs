using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core.Bank;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces{
    public interface IRptftBondRepository : IBankRepository<RptftBond>{
        Task<RptftBond> GetBond(int collatNo);
    }
}