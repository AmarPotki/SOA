using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Bank{
    [DataContract]
    public class GuarantorsDto : IDto{
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string NationalId { get; set; }
        [DataMember]
        public decimal GuarantyRemaining { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}