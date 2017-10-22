using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.DataAccess.Core.VipBanking;
namespace RahyabServices.DataAccess.Repositories.VipBanking.Interfaces{
    public interface IGeneralReportRepository : IVipBankingRepository<GeneralReport>{
        Task<GeneralReport> GetMax();
    }
}