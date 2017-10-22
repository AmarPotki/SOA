using System.Threading.Tasks;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.Cando.Adanic;
using RahyabServices.Business.Services.Intefaces.Cando;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class AdanicRestContract : ContractBase, IAdanicRestContract{
        private readonly IAdanicService _adanicService;
        public AdanicRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer, ILogger logger,
            ISharepointAuthorizationService sharepointAuthorizationService, IAdanicService adanicService)
            : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _adanicService = adanicService;
        }
        public async Task<string> CallService(string key, string name, string variables){
            var call = new CallServiceDtq {Key = key, Name = name,Variables = variables};
            return await
                ValidateThenExecuteFaultHandledOperation<string, CallServiceDtq>(
                    async () => await _adanicService.CallWebService(call), call);
        }
    }
}