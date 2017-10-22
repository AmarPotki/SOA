using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Cando.FaraFek{
    [DataContract]
    public class GetInfoPostDtq : SharepointRequestDto{
        [DataMember]
        public string[] Keys { get; set; }
        [DataMember]
        public string[] Values { get; set; }
    }
}