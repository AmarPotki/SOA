using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts{
    [DataContract]
    public class GetCustomerDelinquentHistoryDto :IDto{
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string PersianDate { get; set; }
    }
}