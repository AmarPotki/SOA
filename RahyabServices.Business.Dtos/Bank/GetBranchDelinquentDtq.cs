using System.Runtime.Serialization;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
namespace RahyabServices.Business.Dtos.Bank{
    [DataContract]
    public class GetBranchDelinquentDtq : ManagerRequestDto{
        [DataMember]
        public string BranchCode { get; set; }
        [DataMember]
        public string PersianFromDate { get; set; }
        [DataMember]
        public string PersianToDate { get; set; }
    }
}