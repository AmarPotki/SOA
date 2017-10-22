using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts.Manager{
    [DataContract]
    public class GetAllCustomerDelinquentsHistoryDto:ManagerRequestDto{
        [DataMember]
        public string PersianDate { get; set; }

    }
}