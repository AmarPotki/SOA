using System.Runtime.Serialization;
using RahyabServices.Common.Dto;

namespace RahyabServices.Business.Dtos.Supplies
{
    [DataContract]
    public class IranNaraChequeRequestDtc: SharepointRequestDto
    {
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public int ItemId { get; set; }

        public string ApproverName { get; set; }

    }
}