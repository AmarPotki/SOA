using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.VipBanking{
    [DataContract]
    public class GetAllVipDelinquentDto : SharepointRequestDto
    {
        [DataMember]
        public int Skip { get; set; }
        [DataMember]
        public int Take { get; set; }

    }
}