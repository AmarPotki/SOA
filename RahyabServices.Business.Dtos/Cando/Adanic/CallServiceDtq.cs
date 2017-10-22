using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Cando.Adanic{
    [DataContract]
    public class CallServiceDtq : SharepointRequestDto{
        [DataMember]
        public string Variables { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}