using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    [DataContract]
    public class GetAccountInformationDtq:SharepointRequestDto{
        [DataMember]
        public string AccountNumber { get; set; }
        [DataMember]
        public string BranchCode { get; set; }
    }
}