using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    [DataContract]
    public class RejectSayadDtc : SharepointRequestDto
    {
        [DataMember]
        public int ItemId { get; set; }
    }
}