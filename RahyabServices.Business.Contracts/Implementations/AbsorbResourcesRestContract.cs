using System.Threading.Tasks;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.AbsorbResources;
using RahyabServices.Business.Services.Intefaces.AbsorbResources;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class AbsorbResourcesRestContract : ContractBase, IAbsorbResourcesRestContract{
        private readonly IAbsorbResourcesService _absorbResources;
        public AbsorbResourcesRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer,
            ILogger logger, ISharepointAuthorizationService sharepointAuthorizationService, IAbsorbResourcesService absorbResources)
            : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _absorbResources = absorbResources;
        }
        public async Task<CustomerInformationDto> GetCustomerInformation(string key, string customerNumber){
            var get = new GetCustomerInformationDtq {Key = key, CustomerNumber = customerNumber};
            return await
                ValidateThenExecuteFaultHandledOperation<CustomerInformationDto, GetCustomerInformationDtq>(
                    async () => await _absorbResources.GetBrifCustomerInformation(get), get);
        }
    }
}