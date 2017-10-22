using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    [DataContract]
    public class InquiryRequestDtc : SharepointRequestDto {
        [DataMember]
        public int ItemId { get; set; }
        [DataMember]
        public string DeliveryBranch { get; set; }
    }
}