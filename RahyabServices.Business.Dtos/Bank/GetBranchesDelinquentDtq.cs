using System.Runtime.Serialization;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
namespace RahyabServices.Business.Dtos.Bank{
    [DataContract]
    public class GetBranchesDelinquentDtq : ManagerRequestDto{
        [DataMember]
        public string PersianFromDate { get; set; }
        [DataMember]
        public string PersianToDate { get; set; }
    }
}