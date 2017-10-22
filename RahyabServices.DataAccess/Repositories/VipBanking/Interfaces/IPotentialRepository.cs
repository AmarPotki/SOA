using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.DataAccess.Core.VipBanking;
namespace RahyabServices.DataAccess.Repositories.VipBanking.Interfaces{
    public interface IPotentialRepository : IVipBankingRepository<Potential>
    {
        Task<IEnumerable<Potential>> GetAll(int skip, int take);
        Task<Potential> GetPotentialByCustomerNumber(string customerNumber);

        Task<IEnumerable<Potential>> GetFiltered(int skip, int take, string filter);
    }
}