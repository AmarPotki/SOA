using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Log.Split;
namespace RahyabServices.Business.Services.Intefaces.Delinquent{
    public interface ISplitService{
        Task AddSplitLogAsync(AddSplitLogDto addSplitLogDto);
        Task<bool> CheckPrivilegeAddSplitLogAsync(AddSplitLogDto addSplitLogDto);
        Task<bool> CheckPrivilegeEditSplitLogAsync(EditSplitLogDto editSplitLogDto);
        Task<bool> CheckPrivilegeEditRequestSplitLogAsync(GetRequestSplitLogDto getRequestSplitLogDto);
        Task RespondRequestSplitAsync(RespondRequestSplitDto respondRequestClearingDto);
        Task<SplitLogDto> GetSplitLogAsync(GetSplitLogDto getSplitLogDto);
        Task<RequestSplitLogDto> GetRequestSplitLogAsync(GetRequestSplitLogDto getSplitLogDto);
        Task EditSplitAsync(EditSplitLogDto editSplitLogDto);
        Task EditRequestSplitLogAsync(EditRequestSplitLogDto editRequestSplitLogDto);
        Task DisableSplitEditingDto(DisableSplitEditingDto disableSplitEditingDto);
        Task CancelRequestSplit(CancelRequestSplitDto cancelRequestSplitDto);
    }
}