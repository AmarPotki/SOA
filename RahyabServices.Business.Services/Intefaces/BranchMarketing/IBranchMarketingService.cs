using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.BranchMarketing;
namespace RahyabServices.Business.Services.Intefaces.BranchMarketing{
    public interface IBranchMarketingService{
        Task<LastBalAcountsDto> GetLastBal(GetLastBalCustomerDto customerDto);
        Task<ResultItemsDto> GetAndSaveItems();
        Task<ResutlDeleteItemDto> RemoveItem(GetDeleteItemDtc getDeleteItemDto);
        Task<ResultItemsDto> CheckSuccessCustomers();
        Task<ResultApproveDtc> ApproveItems(GetApproveItemsDto getApproveItemDto);
    }
}