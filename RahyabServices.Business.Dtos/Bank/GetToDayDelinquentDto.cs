using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Bank{
    [DataContract]
    public class GetToDayDelinquentDto : IDto{
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}