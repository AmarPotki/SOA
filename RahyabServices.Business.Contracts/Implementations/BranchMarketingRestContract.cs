using System.Threading.Tasks;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.BranchMarketing;
using RahyabServices.Business.Dtos.Cando.FaraFek;
using RahyabServices.Business.Services.Intefaces.BranchMarketing;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class BranchMarketingRestContract : ContractBase, IBranchMarketingRestContract{
        private readonly IBranchMarketingService _branchMarketingService;
        public BranchMarketingRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer,
            ILogger logger, ISharepointAuthorizationService sharepointAuthorizationService, IBranchMarketingService branchMarketingService)
            : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _branchMarketingService = branchMarketingService;
        }
        public async Task<LastBalAcountsDto> GetLastBal(GetLastBalCustomerDto getLastBalCustomerDto){
            return await
               ValidateThenExecuteFaultHandledOperation<LastBalAcountsDto, GetLastBalCustomerDto>(
                   async () => await _branchMarketingService.GetLastBal(getLastBalCustomerDto), getLastBalCustomerDto);
        }
        public async Task<ResutlDeleteItemDto> RemoveItem(GetDeleteItemDtc getDeleteItemDto)
        {
            return await
               ValidateThenExecuteFaultHandledOperation<ResutlDeleteItemDto, GetDeleteItemDtc>(
                   async () => await _branchMarketingService.RemoveItem(getDeleteItemDto), getDeleteItemDto);
        }
        public async Task<ResultApproveDtc> ApproveItems(GetApproveItemsDto getApproveItemDto){
            return await
                 ValidateThenExecuteFaultHandledOperation<ResultApproveDtc, GetApproveItemsDto>(
                     async () => await _branchMarketingService.ApproveItems(getApproveItemDto), getApproveItemDto);

        }
    }
}