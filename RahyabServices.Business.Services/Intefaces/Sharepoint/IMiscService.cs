using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Misc;
namespace RahyabServices.Business.Services.Intefaces.Sharepoint{
    public interface IMiscService{
        Task<double> CalculateProfitAsync(CalculateProfitDtc calculateProfitDtc);
    }
}