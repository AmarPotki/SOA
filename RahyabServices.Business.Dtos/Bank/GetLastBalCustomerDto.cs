using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Bank
{
    [DataContract]
    public class GetLastBalCustomerDto:SharepointRequestDto
    {
        [DataMember]
        public string CustomerNumber { get; set; }
        [DataMember]
        public decimal BranchAmount { get; set; }
    }
}
