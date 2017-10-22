using System.Collections.Generic;
using RahyabServices.Business.Dtos.TatCharity;
namespace RahyabServices.Business.Services.Intefaces.TatCharity{
    public interface ITatService{
        IEnumerable<TatApplicantDto> GetTatApplicantsByTitle(string title);
        IEnumerable<TatApplicantDto> GetTatApplicantsByNationalId(string nid);
        IEnumerable<TatApplicantDto> GetTatApplicantsByFileNo(string fn);
        IEnumerable<TatLoanDto> GetUserLoans(string applicantId);
        int GetPaidLoanFundsCount(string loanId);
        int AddPortalFundLoan(AddPortalFundDtc dtc);
        int AddTatFundLoan(AddTatFundDtc dtc);

        IEnumerable<TatPensionDto> GetUserPensions(string applicantId);
        int GetPaidPensionFundsCount(string pensionId);
        int AddPortalFundPension(AddPortalPensionFundDtc dtc);
        int AddTatFundPension(AddTatPensionFundDtc dtc);
    }
}