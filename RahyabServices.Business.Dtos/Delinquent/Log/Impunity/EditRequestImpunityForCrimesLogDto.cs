﻿using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;

namespace RahyabServices.Business.Dtos.Delinquent.Log.Impunity
{
    [DataContract]
    public class EditRequestImpunityForCrimesLogDto : IDto
    {
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string AuthorUserName { get; set; }

        [DataMember]
        public decimal ImpunityAmount { get; set; }
        [DataMember]
        public double InterestRate { get; set; }
        public string UserName
        {
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}