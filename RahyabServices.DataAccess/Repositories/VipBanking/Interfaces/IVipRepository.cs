using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.DataAccess.Core.VipBanking;
namespace RahyabServices.DataAccess.Repositories.VipBanking.Interfaces{
    public interface IVipRepository : IVipBankingRepository<Vip>{
        Task<IEnumerable<Vip>> GetAll(int skip, int take);
        Task<Vip> GetVipByCustomerNumber(string customerNumber);
        Task<IEnumerable<Vip>> GetFiltered(int skip, int take, string filter);
    }
}