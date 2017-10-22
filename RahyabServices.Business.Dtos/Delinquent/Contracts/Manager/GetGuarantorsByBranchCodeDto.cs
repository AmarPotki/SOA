using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts.Manager{
    [DataContract]
    public class GetGuarantorsByBranchCodeDto:ManagerRequestDto{
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public string BranchCode { get; set; }
    }
}