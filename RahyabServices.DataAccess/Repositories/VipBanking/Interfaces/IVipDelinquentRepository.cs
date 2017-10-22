using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.DataAccess.Core.VipBanking;
namespace RahyabServices.DataAccess.Repositories.VipBanking.Interfaces{
    public interface IVipDelinquentRepository : IVipBankingRepository<VipDelinquent>
    {
        Task<IEnumerable<VipDelinquent>> GetAll(int skip, int take);
        Task<IEnumerable<VipDelinquent>> GetDelinquents(string customerNumber, int skip, int take);
        Task<int> GetDelinquentsCount(string customerNumber);
    }
}