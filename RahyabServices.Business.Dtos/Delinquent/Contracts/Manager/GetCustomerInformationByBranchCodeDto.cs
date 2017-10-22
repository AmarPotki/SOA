using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts.Manager{
    [DataContract]
    public class GetCustomerInformationByBranchCodeDto : ManagerRequestDto{
        [DataMember]
        public string CustomerNumber { get; set; }
        [DataMember]
        public string BranchCode { get; set; }
    }
}