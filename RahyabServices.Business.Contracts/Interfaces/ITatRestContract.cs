using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using RahyabServices.Business.Dtos.TatCharity;
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface ITatRestContract{
        [OperationContract]
        [WebGet(UriTemplate = "GetApplicantsByTitle/{title}")]
        IEnumerable<TatApplicantDto> GetApplicantsByTitle(string title);
        [OperationContract]
        [WebGet(UriTemplate = "GetApplicantsByNationalCode/{nationalCode}")]
        IEnumerable<TatApplicantDto> GetApplicantsByNationalCode(string nationalCode);
        [OperationContract]
        [WebGet(UriTemplate = "GetApplicantsByFileNo/{fileNo}")]
        IEnumerable<TatApplicantDto> GetApplicantsByFileNo(string fileNo);
        [OperationContract]
        [WebGet(UriTemplate = "GetApplicantLoans/{applicantId}")]
        IEnumerable<TatLoanDto> GetApplicantLoans(string applicantId);
        [OperationContract]
        [WebGet(UriTemplate = "GetPaidLoanFundsCount/{loanId}")]
        int GetPaidLoanFundsCount(string loanId);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddPortalFundLoan", RequestFormat = WebMessageFormat.Json)]
        int AddPortalFundLoan(AddPortalFundDtc dtc);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddTatFundLoan", RequestFormat = WebMessageFormat.Json)]
        int AddTatFundLoan(AddTatFundDtc dtc);

        [OperationContract]
        [WebGet(UriTemplate = "GetApplicantPensions/{applicantId}")]
        IEnumerable<TatPensionDto> GetApplicantPensions(string applicantId);
        [OperationContract]
        [WebGet(UriTemplate = "GetPaidPensionFundsCount/{pensionId}")]
        int GetPaidPensionFundsCount(string pensionId);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddPortalFundPension", RequestFormat = WebMessageFormat.Json)]
        int AddPortalFundPension(AddPortalPensionFundDtc dtc);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddTatFundPension", RequestFormat = WebMessageFormat.Json)]
        int AddTatFundPension(AddTatPensionFundDtc dtc);
    }
}