using System.Collections.Generic;
using System.Runtime.Serialization;
using RahyabServices.Business.Dtos.Kendo;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.VipBanking{
    [DataContract]
    public class GetAllVipDto : SharepointRequestDto{
        [DataMember]
        public int Skip { get; set; }
        [DataMember]
        public int Take { get; set; }
        [DataMember]
        public IEnumerable<FilterDto> Filter { get; set; }
        [DataMember]
        public IEnumerable<SortDto> Sort { get; set; }
    }
}