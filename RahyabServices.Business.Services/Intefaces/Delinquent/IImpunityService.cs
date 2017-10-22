using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Impunity;

namespace RahyabServices.Business.Services.Intefaces.Delinquent
{
    public interface IImpunityService
    {
        Task<ImpunityForCrimesLogDto> GetImpunityAsync(GetImpunityLogDto impunityLogDto);
        Task<RequestImpunityForCrimesLogDto> GetRequestImpunityAsync(GetRequestImpunityLogDto impunityLogDto);
        Task AddImpunityForCrimesLogAsync(AddImpunityForCrimesLogDto addImpunityForCrimesLogDto);
        Task EditImpunityForCrimesLogAsync(EditImpunityForCrimesLogDto editImpunityForCrimesLogDto);
        Task EditRequestImpunityForCrimesLogAsync(EditRequestImpunityForCrimesLogDto editRequestImpunityForCrimesLogDto);
        Task RespondRequestImpunityForCrimesAsync(RespondRequestImpunityForCrimesDto respondRequestImpunityForCrimesDto);
        Task<bool> CheckPrivilegeAddImpunityLogAsync(AddImpunityForCrimesLogDto impunityForCrimesLogDto);
        Task<bool> CheckPrivilegeEditImpunityLogAsync(EditImpunityForCrimesLogDto editSplitLogDto);
        Task<bool> CheckPrivilegeEditRequestImpunityLogAsync(GetRequestImpunityLogDto getRequestImpunityLogDto);
        Task DisableImpunityEditingDto(DisableImpunityEditingDto disableImpunityEditingDto);
    }
}
