using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Log.Renewal;
namespace RahyabServices.Business.Services.Intefaces.Delinquent{
    public interface IRenewalService{
        Task AddRenewalLogAsync(AddRenewalLogDto dto);
        Task<bool> CheckPrivilegeAddRenewalLogAsync(AddRenewalLogDto dto);
        Task<bool> CheckPrivilegeEditRenewalLogAsync(EditRenewalLogDto dto);
        Task<RenewalLogDto> GetRenewalLogAsync(GetRenewalLogDto dto);
        Task EditRenewalAsync(EditRenewalLogDto editRenewalLogDto);
    }
}