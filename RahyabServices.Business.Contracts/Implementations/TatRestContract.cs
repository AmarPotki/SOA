using System.Collections.Generic;
using FluentValidation;
using RahyabServices.Business.Contracts.Interfaces;
using RahyabServices.Business.Dtos.TatCharity;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.Business.Services.Intefaces.TatCharity;
using RahyabServices.Common.Cryptography;
using RahyabServices.Common.Logging;
namespace RahyabServices.Business.Contracts.Implementations{
    public class TatRestContract : ContractBase, ITatRestContract{
        private readonly ITatService _tatService;
        public TatRestContract(IValidatorFactory validatorFactory, ICryptographer cryptographer,
            ILogger logger, ISharepointAuthorizationService sharepointAuthorizationService, ITatService tatService)
            : base(validatorFactory, cryptographer, logger, sharepointAuthorizationService){
            _tatService = tatService;
        }
        public IEnumerable<TatApplicantDto> GetApplicantsByTitle(string title){
            return _tatService.GetTatApplicantsByTitle(title);
        }
        public IEnumerable<TatApplicantDto> GetApplicantsByNationalCode(string nationalCode){
            return _tatService.GetTatApplicantsByNationalId(nationalCode);
        }
        public IEnumerable<TatApplicantDto> GetApplicantsByFileNo(string fileNo){
            return _tatService.GetTatApplicantsByFileNo(fileNo);
        }
        public IEnumerable<TatLoanDto> GetApplicantLoans(string applicantId){
            return _tatService.GetUserLoans(applicantId);
        }
        public int GetPaidLoanFundsCount(string loanId){

            return _tatService.GetPaidLoanFundsCount(loanId);
        }
        public int AddPortalFundLoan(AddPortalFundDtc dtc){
            return _tatService.AddPortalFundLoan(dtc);
        }
        public int AddTatFundLoan(AddTatFundDtc dtc){
            return _tatService.AddTatFundLoan(dtc);
        }
        public IEnumerable<TatPensionDto> GetApplicantPensions(string applicantId){
            return _tatService.GetUserPensions(applicantId);
        }
        public int GetPaidPensionFundsCount(string pensionId){
            return _tatService.GetPaidPensionFundsCount(pensionId);
        }
        public int AddPortalFundPension(AddPortalPensionFundDtc dtc){
            return _tatService.AddPortalFundPension(dtc);
        }
        public int AddTatFundPension(AddTatPensionFundDtc dtc){
            return _tatService.AddTatFundPension(dtc);
        }
    }
}