using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.BranchMarketing;
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface IBranchMarketingRestContract{
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getLastBal")]
        Task<LastBalAcountsDto> GetLastBal(GetLastBalCustomerDto getLastBalCustomerDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "removeItem")]
        Task<ResutlDeleteItemDto> RemoveItem(GetDeleteItemDtc getDeleteItemDto);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "approveItems")]
        Task<ResultApproveDtc> ApproveItems(GetApproveItemsDto getApproveItemDto);
    }
}