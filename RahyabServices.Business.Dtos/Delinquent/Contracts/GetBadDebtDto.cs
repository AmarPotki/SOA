﻿using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts{
    [DataContract]
    public class GetBadDebtDto:IDto{
        [DataMember]
        public string UserName { get; set; }
    }
}