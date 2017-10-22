using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Cando.Adanic;
namespace RahyabServices.Business.Facades.Interfaces{
    public interface IAdanicFacade{
        Task<string> CallService(CallServiceDtq dtq);
    }
}