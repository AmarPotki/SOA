using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface IAdanicRestContract
    {
        [OperationContract]
        [WebGet(UriTemplate = "callService/{key}/{name}/{variables}")]
        Task<string> CallService(string key,string name, string variables);
    }
}