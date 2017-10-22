using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Customer{
    [DataContract]
    public class GetCustomerInformationDto : IDto{
        [DataMember]
        public string CustomerNumber { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}