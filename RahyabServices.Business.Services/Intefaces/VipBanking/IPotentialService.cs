using System.Threading.Tasks;
using RahyabServices.Business.Dtos.VipBanking;
namespace RahyabServices.Business.Services.Intefaces.VipBanking{
    public interface IPotentialService
    {
        Task<AllPotentialDto> GetAll(GetAllPotentialDto getAllPotentialDto);
        Task<PotentialDto> GetPotentialByCustomerNumber(GetPotentialByCustomerNumberDtq getPotential);
    }
}