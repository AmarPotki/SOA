using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Cando.FaraFek{
    [DataContract]
    public class GetDiagramDtq : SharepointRequestDto{
        public string Name { get; set; }
    }
}