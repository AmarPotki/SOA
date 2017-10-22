using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.VipBanking;
using RahyabServices.Business.Dtos.VipBanking;
namespace RahyabServices.Business.Services.Intefaces.VipBanking{
    public interface IGeneralReportService{
        Task<GeneralReportDto> GetMax();
    }
}