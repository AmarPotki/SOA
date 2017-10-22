using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.DataAccess.Core.VipBanking;
namespace RahyabServices.DataAccess.Repositories.VipBanking.Interfaces{
    public interface IChequeRepository : IVipBankingRepository<Cheque>
    {
        Task<IEnumerable<Cheque>> GetAll(int skip, int take);
        Task<IEnumerable<Cheque>> GetCheques(string customerNumber, int skip, int take);
        Task<int> GetChequesCount(string customerNumber);
    }
}