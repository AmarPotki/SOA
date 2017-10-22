using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.BranchMarketing
{
    [DataContract]
    public class GetApproveItemsDto :SharepointRequestDto
    {
        [DataMember]
        public string CampainId { get; set; }
    }
}
