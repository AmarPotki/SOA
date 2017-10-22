using System.ServiceModel;
namespace RahyabServices.Business.Contracts.Interfaces{
    [ServiceContract]
    public interface IMiscContract{
        [OperationContract]
        bool OperattionCreateCsvFile(string key);
    }
}