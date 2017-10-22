using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.ClearingLog{
    [DataContract]
    public class RequestClearingLogDto : IDto
    {
        public string UserName { get; set; }
        [DataMember]
        public string AuthorUserName { get; set; }        
        [DataMember]
        public double TheAmountOfAssessment { get; set; }

        [DataMember]
        public double InterestRate { get; set; }

        [DataMember]
        public DateTime LegislationDate { get; set; }

    }
}