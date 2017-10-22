using System.Threading.Tasks;
using RahyabServices.Business.Dtos.VipBanking;
namespace RahyabServices.Business.Services.Intefaces.VipBanking{
    public interface IChequeService
    {
        Task<AllChequeDto> GetAll(GetAllChequeDto getAllChequeDto);
        Task<AllChequeDto> GetCheques(GetChequesDto getCheques);
    }
}