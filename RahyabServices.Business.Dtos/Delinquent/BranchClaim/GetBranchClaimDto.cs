using System;
using System.Runtime.Serialization;
using RahyabServices.Business.Dtos.Delinquent.Contracts.Manager;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.BranchClaim{
    [DataContract]
    public class GetBranchClaimDto:ManagerRequestDto{
        [DataMember]
        public DateTime DateOnly { get; set; }
        public int BranchId { get; set; }
    }
}