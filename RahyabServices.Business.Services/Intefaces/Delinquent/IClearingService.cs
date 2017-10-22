using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog;
namespace RahyabServices.Business.Services.Intefaces.Delinquent{
    public interface IClearingService{
        Task AddClearingLogAsync(AddClearingLogDto addClearingLogDto);
        Task<ClearingLogDto> GetClearingAsync(GetClearingLogDto clearingLogDto);
        Task<RequestClearingLogDto> GetRequestClearingAsync(GetRequestClearingLogDto getRequestClearingLogDto);
        Task RespondRequestClearingAsync(RespondRequestClearingDto respondRequestClearingDto);
        Task EditClearingAsync(EditClearingLogDto editClearingLogDto);
        Task EditRequestClearingAsync(EditRequestClearingLogDto editClearingLogDto);
        Task RemoveRequestClearingAsync(RemoveClearingLogDto removeClearingLogDto);
        Task<bool> CheckPrivilegeAddClearingLogAsync(AddClearingLogDto addClearingLogDto);
        Task<bool> CheckPrivilegeEditClearingLogAsync(EditClearingLogDto editClearingLogDto);
        Task DisableClearingEditingDto(DisableClearingEditingDto disableClearingEditingDto);
        Task<bool> CheckPrivilegeEditRequestClearingLogAsync(GetRequestClearingLogDto getRequestClearingLogDto);
    }
}