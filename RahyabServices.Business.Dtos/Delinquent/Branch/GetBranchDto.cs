using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Branch{
    [DataContract]
    public class GetBranchDto : IDto{
        [DataMember]
        public string UserName { get; set; }
    }
}