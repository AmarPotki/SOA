using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.VipBanking{
    [DataContract]
    public class GetVipByCustomerNumberDtq : SharepointRequestDto{
        [DataMember]
        public string CustomerNumber { get; set; }
    }
}