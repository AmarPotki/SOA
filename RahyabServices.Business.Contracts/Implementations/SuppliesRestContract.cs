using System.Threading.Tasks;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Business.Services.Intefaces.Supplies;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class SuppliesRestContract : ContractBase, ISuppliesRestContract{
        private readonly ISuppliesService _suppliesService;
        public SuppliesRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer, ILogger logger,
             ISharepointAuthorizationService sharepointAuthorizationService, ISuppliesService suppliesService) :
                base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _suppliesService = suppliesService;
        }
        public async Task<bool> CentralBankInquiry(InquiryRequestDtc inquiryRequestDtc){

            return await
                ValidateThenExecuteFaultHandledOperation<bool, InquiryRequestDtc>(
                    async () => await _suppliesService.Inquiry(inquiryRequestDtc), inquiryRequestDtc);
         
        }
        public async Task<BriefAccountInformationDto> GetAccountInformation(string key, string accountNumber, string branchCode)
        {
            var accountInformationDtq = new GetAccountInformationDtq {Key = key,AccountNumber = accountNumber,BranchCode = branchCode};
            return await
                ValidateThenExecuteFaultHandledOperation<BriefAccountInformationDto, GetAccountInformationDtq>(
                    async () => await _suppliesService.BriefAccountInformation(accountInformationDtq), accountInformationDtq);
        }

        public async Task<AccountInformationDto> GetAccountFullInformation(string key, string accountNumber,string branchCode){
            var accountInformationDtq = new GetAccountInformationDtq { Key = key, AccountNumber = accountNumber, BranchCode = branchCode };
            return await
                ValidateThenExecuteFaultHandledOperation<AccountInformationDto, GetAccountInformationDtq>(
                    async () => await _suppliesService.GetAccountInformation(accountInformationDtq), accountInformationDtq);
        }
        public async Task<bool> IsValidCustomerInformation(string key, string accountNumber){
            var isValidKarizSingerDtq = new IsValidCustomerInformationDtq { Key = key, AccountNumber = accountNumber};
            //! proccess dar validation anjam mishavad
            return await
                ValidateThenExecuteFaultHandledOperation<bool, IsValidCustomerInformationDtq>(
                    async () => await _suppliesService.DoNothing(isValidKarizSingerDtq), isValidKarizSingerDtq);
        }
        public async Task<bool> AcceptCheque(AcceptSayadDtc acceptSayadDtc){
            return await
                ValidateThenExecuteFaultHandledOperation<bool, AcceptSayadDtc>(
                    async () => await _suppliesService.Accept(acceptSayadDtc), acceptSayadDtc); 
        }
        public async Task<bool> RejectCheque(RejectSayadDtc rejectSayadDtc){
            return await
                 ValidateThenExecuteFaultHandledOperation<bool, RejectSayadDtc>(
                     async () => await _suppliesService.Reject(rejectSayadDtc), rejectSayadDtc);
        }
        public async Task<bool> RejectChequeByAdmin(RejectSayadDtc rejectSayadDtc)
        {
            return await
                 ValidateThenExecuteFaultHandledOperation<bool, RejectSayadDtc>(
                     async () => await _suppliesService.RejectByAdmin(rejectSayadDtc), rejectSayadDtc);
        }

        public async  Task<bool> DeactivateBaseIbanRequest(DeactivateBaseIBANDtc deactivateBaseIbanDtc){
            return await
                ValidateThenExecuteFaultHandledOperation<bool, DeactivateBaseIBANDtc>(
                    async () => await _suppliesService.DeactivateBaseIbanRequest(deactivateBaseIbanDtc),
                    deactivateBaseIbanDtc);

        }
        public async Task<bool> IranNaraChequeRequest(IranNaraChequeRequestDtc chequeRequestDtc){
            return await
                ValidateThenExecuteFaultHandledOperation<bool, IranNaraChequeRequestDtc>(
                    async () => await _suppliesService.IranNaraChequeRequest(chequeRequestDtc),
                    chequeRequestDtc);
        }
    }
}