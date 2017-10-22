using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Finance;
namespace RahyabServices.Business.Services.Intefaces.Sharepoint{
    public interface IFinanceService{
        Task<string> GetDailyReport(GetDailyliquidityReportDtq dtq);
        Task<string> GetReportFacility(GetReportFacilityDetailDtq getDaily);
    }
}