using System.Runtime.Serialization;
namespace RahyabServices.Common.Exceptions{
    [DataContract]
    public class AuthenticationFault
    {
        [DataMember]
        public string Message { get; set; }

        public AuthenticationFault(string message)
        {
            Message = message;
        }

    }
}