using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Bank{
    [DataContract]
    public class BranchDelinquentDto{
        [DataMember]
        public string Contract { get; set; }
        [DataMember]
        public long Amount { get; set; }
    }
}