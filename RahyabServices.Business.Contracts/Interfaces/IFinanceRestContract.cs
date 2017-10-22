using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.BankPerson;
using RahyabServices.Business.Dtos.Finance;
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface IFinanceRestContract{
        [OperationContract]
        [WebGet(UriTemplate = "DailyliquidityReport/{key}/{fromDate}/{toDate}")]
        Task<string> GetDailyliquidityReport(string key, string fromDate, string toDate);
        [OperationContract]
        [WebGet(UriTemplate = "ReportFacilityDetail/{key}/{fromDate}/{toDate}")]
        Task<string> GetReportFacilityDetail(string key, string fromDate, string toDate);
    }
}