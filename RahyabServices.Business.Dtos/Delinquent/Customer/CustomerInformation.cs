using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Customer{
    [DataContract]
    public class CustomerInformationDto : IDto{
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string CellPhone { get; set; }
        [DataMember]
        public string NationalCode { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}