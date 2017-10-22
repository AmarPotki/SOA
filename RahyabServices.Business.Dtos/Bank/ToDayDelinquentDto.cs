using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Bank{
    [DataContract]
    public class ToDayDelinquentDto : IDto{
        [DataMember]
        public string BranchCode { get; set; }
        [DataMember]
        public string BranchName { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}