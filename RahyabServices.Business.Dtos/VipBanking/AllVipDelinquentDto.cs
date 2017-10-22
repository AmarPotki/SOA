using System.Collections.Generic;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.VipBanking{
    [DataContract]
    public class AllVipDelinquentDto : IDto
    {
        [DataMember]
        public int Total { get; set; }
        [DataMember]
        public IEnumerable<VipDelinquentDto> DelinquentDtos { get; set; }
    }
}