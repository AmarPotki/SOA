using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts.Manager{
    [DataContract]
    public class GetCustomerDelinquentHistoryByBranchCodeDto:ManagerRequestDto{
        [DataMember]
        public string PersianDate { get; set; }
        [DataMember]
        public string BranchCode { get; set; }
    }
}