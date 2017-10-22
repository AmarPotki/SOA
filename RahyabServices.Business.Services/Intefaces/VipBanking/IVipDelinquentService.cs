using System.Threading.Tasks;
using RahyabServices.Business.Dtos.VipBanking;
namespace RahyabServices.Business.Services.Intefaces.VipBanking{
    public interface IVipDelinquentService{
        Task<AllVipDelinquentDto> GetAll(GetAllVipDelinquentDto getAllVipDelinquentDto);
        Task<AllVipDelinquentDto> GetDelinquents(GetVipDelinquentsDto getDelinquents);
    }
}