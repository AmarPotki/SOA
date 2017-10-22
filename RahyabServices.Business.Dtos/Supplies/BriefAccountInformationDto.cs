using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    [DataContract]
    public class BriefAccountInformationDto :IDto
    {
        [DataMember]
        public string OpennerBranch { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string CustomerNumber { get; set; }
        [DataMember]
        public string Sheba { get; set; }
        [DataMember]
        public string CustType { get; set; }
        [DataMember]
        public string NationalId { get; set; }
    }
}