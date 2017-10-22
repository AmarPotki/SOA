using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.ParsLogic{
    [DataContract]
    public class TaslListFilterDtc : SharepointRequestDto{
        [DataMember]
        public int ActivityId { get; set; }
        [DataMember]
        public int ActivityType { get; set; }
    }
}