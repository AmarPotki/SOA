using System.Threading.Tasks;
using RahyabServices.Business.Dtos.VipBanking;
namespace RahyabServices.Business.Services.Intefaces.VipBanking
{
    public interface IVipService
    {
        Task<AllVipDto> GetAll(GetAllVipDto getAllVipDto);
        Task<VipDto> GetVipByCustomerNumber(GetVipByCustomerNumberDtq getVipByCustomerNumberDtq);
    }
}