using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Cando.Adanic;
namespace RahyabServices.Business.Services.Intefaces.Cando{
    public interface IAdanicService{
        Task<string> CallWebService(CallServiceDtq serviceDtq);
    }
}