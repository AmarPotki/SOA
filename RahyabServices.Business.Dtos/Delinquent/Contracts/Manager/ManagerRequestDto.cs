﻿using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts.Manager{
    [DataContract]
    public class ManagerRequestDto:IDto{
        [DataMember]
        public string UserName { get; set; }
    }
}