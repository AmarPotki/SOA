using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance;

namespace RahyabServices.Business.Services.Intefaces.Delinquent
{
    public interface IGivingAChanceService
    {
        Task<GivingAChanceLogDto> GetGivingAChanceAsync(GetGivingAChanceLogDto givingAChanceLogDto);
        Task<RequestGivingAChanceLogDto> GetRequestGivingAChanceAsync(GetRequestGivingAChanceLogDto givingAChanceLogDto);
        Task AddGivingAChanceLogAsync(AddGivingAChanceLogDto addGivingAChanceLogDto);
        Task EditGivingAChanceLogAsync(EditGivingAChanceLogDto editGivingAChanceLogDto);
        Task EditRequestGivingAChanceLogAsync(EditRequestGivingAChanceLogDto editRequestGivingAChanceLogDto);
        Task RespondGivingAChanceAsync(RespondRequestGivingAChanceDto respondRequestGivingAChanceDto);
        Task<bool> CheckPrivilegeAddGivingAChanceLogAsync(AddGivingAChanceLogDto addGivingAChanceLogDto);
        Task<bool> CheckPrivilegeEditGivingAChanceLogAsync(EditGivingAChanceLogDto editGivingAChanceLogDto);
        Task<bool> CheckPrivilegeEditRequestGivingAchanceLogAsync(GetRequestGivingAChanceLogDto getRequestGivingAChanceLogDto);

        Task DisableGivingAChanceEditingDto(DisableGivingAChanceEditingDto disableGivingAChanceEditingDto);
    }
}
