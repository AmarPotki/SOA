using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using RahyabServices.Business.Dtos.Kendo;
using System.Threading.Tasks;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.VipBanking
{
    [DataContract]
   public class GetAllPotentialDto : SharepointRequestDto
    {
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
