using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.AbsorbResources;
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface IAbsorbResourcesRestContract{
        [OperationContract]
        [WebGet(UriTemplate = "getCustomerInformation/{key}/{customerNumber}")]
        Task<CustomerInformationDto> GetCustomerInformation(string key,string customerNumber);
    }
}