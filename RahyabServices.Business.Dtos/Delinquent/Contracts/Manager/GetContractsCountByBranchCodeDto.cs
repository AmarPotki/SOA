﻿using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts.Manager{
    [DataContract]
    public class GetContractsCountByBranchCodeDto : ManagerRequestDto{
        [DataMember]
        public string BranchCode { get; set; }
    }
}