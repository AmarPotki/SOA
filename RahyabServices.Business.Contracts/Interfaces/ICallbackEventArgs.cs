using System.Runtime.Serialization;
using System.ServiceModel;

namespace RahyabServices.Business.Contracts.Interfaces
{
    [ServiceContract]
    public interface ICallbackEventArgs
    {
        [DataMember]
        string Method { get; set; }

        [DataMember]
        string Message { get; set; }

        [DataMember]
        object Result { get; set; }
    }
}