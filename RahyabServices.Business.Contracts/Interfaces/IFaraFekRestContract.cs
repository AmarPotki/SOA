using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Cando.FaraFek;
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface IFaraFekRestContract{
        [OperationContract]
        [WebGet(UriTemplate = "diagram/{key}/{name}")]
        Task<string> GetDiagram(string key,string name);

        [OperationContract]
        [WebGet(UriTemplate = "getInfo/{key}")]
        Task<string> GetInfo(string key);
        [OperationContract]
        [WebGet(UriTemplate = "getPlotCsvPost/{key}")]
        Task<string> GetPlotCsv(string key);

        [OperationContract]
        [WebGet(UriTemplate = "getDashbordHtml/{key}")]
        Task<string> GetDashbordHtml(string key);
        [OperationContract]
        [WebGet(UriTemplate = "getDashbordData/{key}")]
        Task<string> GetDashbordData(string key);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "diagram")]
        Task<string> GetDiagramPost(GetDiagramPostDtq getDiagramPostDtq);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getInfo")]
        Task<string> GetInfoPost(GetInfoPostDtq getInfoPostDtq);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "getPlotCsvPost")]
        Task<string> GetPlotCsvPost(GetPlotCsvPostDtq getPlotCsvPostDtq);
    }
}