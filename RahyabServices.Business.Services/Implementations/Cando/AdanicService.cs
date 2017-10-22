using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Cando.Adanic;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Cando;
namespace RahyabServices.Business.Services.Implementations.Cando{
    public class AdanicService : IAdanicService{
        private readonly IAdanicFacade _adanicFacade;
        public AdanicService(IAdanicFacade adanicFacade){
            _adanicFacade = adanicFacade;
        }
        public async Task<string> CallWebService(CallServiceDtq serviceDtq){
            serviceDtq.Variables = "?" + serviceDtq.Variables.Replace("and", "&");
          return  await _adanicFacade.CallService(serviceDtq);
        }
    }
}