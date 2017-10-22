using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.VipBanking{
    [DataContract]
    public class GetVipDelinquentsDto : SharepointRequestDto
    {
        [DataMember]
        public string CustomerNumber { get; set; }
        [DataMember]
        public int Skip { get; set; }
        [DataMember]
        public int Take { get; set; }

    }
}