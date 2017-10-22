using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    [DataContract]
    public class IsValidCustomerInformationDtq : SharepointRequestDto{
        [DataMember]
        public string AccountNumber { get; set; }

    }
}