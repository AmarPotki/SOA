using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Supplies;
using RahyabServices.Business.Facades.SayadWithSSL;
namespace RahyabServices.Business.Facades.Interfaces{
    public interface ISayadFacade{
        RequestResponse CallInsertChequeBookRequest(AccountInformationDto accountInformationDto);
        Task<GetChequeBookStatusByInquiryCodeResponse> Check(string code);
        Task<UpdateAcceptInquiryResponse> Accept(string code, string inquiryTypeCode);
        Task<UpdateRejectInquiryResponse> Reject(string code);
        Task<DeactivateBaseIBANResponse> DeActivateBaseAccount(string sheba);
    }
}