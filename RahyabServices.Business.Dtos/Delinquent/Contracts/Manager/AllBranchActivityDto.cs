using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts.Manager{
    [DataContract]
    public class AllBranchActivityDto : IDto {
        public string UserName { get; set; }
        [DataMember]
        public string BranchCode { get; set; }
        [DataMember]
        public string BranchName { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public int Total { get; set; }
    }
}