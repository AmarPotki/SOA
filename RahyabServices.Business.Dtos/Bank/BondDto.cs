using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Bank{
    [DataContract]
    public class BondDto:IDto{
        [DataMember]
        public int BondType { get; set; }
        [DataMember]
        public decimal Value { get; set; }
        [DataMember]
        public string RegisterDate { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}