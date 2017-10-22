using System.Collections.Generic;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.VipBanking{
    [DataContract]
    public class AllChequeDto : IDto
    {
        [DataMember]
        public int Total { get; set; }
        [DataMember]
        public IEnumerable<VipChequeDto> ChequeDtos { get; set; }
    }
}