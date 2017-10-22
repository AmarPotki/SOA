using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.BranchClaim{
    [DataContract]
    public class BranchClaimDto:IDto{
        public string UserName { get; set; }
        [DataMember]
        public string Created { get; set; }
        [DataMember]
        public double TotalRemainingAmount { get; set; }
        [DataMember]
        public double TotalApprovedAmount { get; set; }
        [DataMember]
        public double TotalCount { get; set; }
        [DataMember]
        public double DebtRemainingAmount { get; set; }
        [DataMember]
        public double DebtApprovedAmount { get; set; }
        [DataMember]
        public double DebtCount { get; set; }

    }
}