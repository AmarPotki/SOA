using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Ebanking;
namespace RahyabServices.Business.Services.Intefaces.Sharepoint{
    public interface IEbankingService{
        Task<string> GetThursdayShift(GetThursdayShiftDtq dtq);
    }
}