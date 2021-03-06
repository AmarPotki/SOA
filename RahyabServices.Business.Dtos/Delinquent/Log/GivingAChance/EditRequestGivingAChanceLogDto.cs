﻿using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;

namespace RahyabServices.Business.Dtos.Delinquent.Log.GivingAChance
{
    [DataContract]
    public class EditRequestGivingAChanceLogDto : IDto
    {
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string AuthorUserName { get; set; }
        [DataMember]
        public decimal DepositAmount { get; set; }

        [DataMember]
        public DateTime LegislationDate { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public DateTime ExpireDate { get; set; }
        public string UserName
        {
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}