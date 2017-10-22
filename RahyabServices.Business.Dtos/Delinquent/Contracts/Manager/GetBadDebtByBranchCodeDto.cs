﻿using System.Runtime.Serialization;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts.Manager{
    [DataContract]
    public class GetBadDebtByBranchCodeDto : ManagerRequestDto{
        [DataMember]
        public string BranchCode { get; set; }
    }
}