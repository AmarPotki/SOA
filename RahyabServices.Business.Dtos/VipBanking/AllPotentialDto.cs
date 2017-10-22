using System.Collections.Generic;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;

namespace RahyabServices.Business.Dtos.VipBanking
{
    [DataContract]
    public class AllPotentialDto : IDto
    {
        [DataMember]
        public int Total { get; set; }
        [DataMember]
        public IEnumerable<PotentialDto> PotentialDtos { get; set; }
    }
}