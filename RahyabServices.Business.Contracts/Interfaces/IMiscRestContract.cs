using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Branch;
using RahyabServices.Business.Dtos.Misc;
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface IMiscRestContract{
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CalculateProfit")]
        Task<double> CalculateProfit(CalculateProfitDtc calculateProfitDtc);

        [OperationContract]
        [WebGet(UriTemplate = "GetThursdayShift/{key}/{yearId}/{monthId}/{workSectionId}")]
        Task<string> GetThursdayShift(string key, string yearId, string monthId, string workSectionId);

        [OperationContract]
        [WebGet(UriTemplate = "operattionCreateCsvFile/{key}")]
        bool OperattionCreateCsvFile(string key);
       
    }
}