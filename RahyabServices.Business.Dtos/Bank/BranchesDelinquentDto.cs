using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Bank{
    [DataContract]
    public class BranchesDelinquentDto{
        [DataMember]
        public string Contract { get; set; }
        [DataMember]
        public long Amount { get; set; }
        [DataMember]
        public string BranchCode { get; set; }
    }
}