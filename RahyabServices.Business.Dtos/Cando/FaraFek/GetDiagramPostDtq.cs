using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Cando.FaraFek{
    [DataContract]
    public class GetDiagramPostDtq : SharepointRequestDto{
        [DataMember]
        public string Name { get; set; }  
        [DataMember]
        public string[] Keys { get; set; }
        [DataMember]
        public string[] Values { get; set; }
    }
}