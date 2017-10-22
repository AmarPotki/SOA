﻿using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Bank{
    [DataContract]
    public class GetBondsDto:IDto{
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public string UserName { get; set; }
    }
}