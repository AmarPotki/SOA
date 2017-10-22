using System.Threading.Tasks;
using RahyabServices.Business.Dtos;
using RahyabServices.Business.Dtos.Supplies;
namespace RahyabServices.Business.Services.Intefaces.Supplies{
    public interface ISuppliesService{
        Task<AccountInformationDto> GetAccountInformation(GetAccountInformationDtq dtq);
        Task<BriefAccountInformationDto> BriefAccountInformation(GetAccountInformationDtq dtq);
        Task<bool> Inquiry(InquiryRequestDtc inquiryRequestDtc);
        Task<bool> CheckSayadState();
        Task<CheckSayadStateResultDto> CheckSayadState(CheckSayadStateDtq dtq);
        Task<bool> RetryInquiry();
        Task<bool> Accept(AcceptSayadDtc acceptSayadDtc);
        Task<bool> Reject(RejectSayadDtc rejectSayadDtc);
        Task<bool> RejectByAdmin(RejectSayadDtc rejectSayadDtc);
        Task<bool> DeactivateBaseIbanRequest(DeactivateBaseIBANDtc deactivateBaseIbanDtc);
        Task<bool> IranNaraChequeRequest(IranNaraChequeRequestDtc iranNaraChequeRequestDtc);
        Task<bool> CheckIranNaraState();
        Task<bool> IsValidKarizSinger(IsValidCustomerInformationDtq isValidCustomerInformationDtq);
        Task<ErrorInfoDto> CheckAccountInformation(string accountNumber);
        Task<ErrorInfoDto> CheckAccountDuplicate(string accountNumber);
        Task<bool> DoNothing(IsValidCustomerInformationDtq isValidKarizSingerDtq);
    }
}