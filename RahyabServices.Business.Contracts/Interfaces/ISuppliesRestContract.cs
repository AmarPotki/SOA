using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Supplies;
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface ISuppliesRestContract{
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CentralBankInquiry")]
        Task<bool> CentralBankInquiry(InquiryRequestDtc inquiryRequestDtc);
        [OperationContract]
        [WebGet(UriTemplate = "GetAccountInformation/{key}/{accountNumber}/{branchCode}")]
        Task<BriefAccountInformationDto> GetAccountInformation(string key,string accountNumber,string branchCode);

        [OperationContract]
        [WebGet(UriTemplate = "GetAccountFullInformation/{key}/{accountNumber}/{branchCode}")]
        Task<AccountInformationDto> GetAccountFullInformation(string key, string accountNumber, string branchCode);
        [OperationContract]
        [WebGet(UriTemplate = "IsValidCustomerInformation/{key}/{accountNumber}")]
        Task<bool> IsValidCustomerInformation(string key, string accountNumber);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AcceptCheque")]
        Task<bool> AcceptCheque(AcceptSayadDtc acceptSayadDtc);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RejectCheque")]
        Task<bool> RejectCheque(RejectSayadDtc rejectSayadDtc);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RejectChequeByAdmin")]
        Task<bool> RejectChequeByAdmin(RejectSayadDtc rejectSayadDtc);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeactivateBaseIbanRequest")]
        Task<bool> DeactivateBaseIbanRequest(DeactivateBaseIBANDtc deactivateBaseIbanDtc);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "IranNaraChequeRequest")]
        Task<bool> IranNaraChequeRequest(IranNaraChequeRequestDtc chequeRequestDtc);
    }
}