using System.ServiceModel;
using RahyabServices.Business.Contracts.Implementations;

namespace RahyabServices.Business.Contracts.Interfaces
{
    [ServiceContract]
    public interface IDelinquentCoreServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void Callback(CallbackEventArgs args);
    }
}