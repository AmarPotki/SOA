using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    [DataContract]
    public class AcceptSayadDtc : SharepointRequestDto{
        [DataMember]
        public int ItemId { get; set; }
    }
}