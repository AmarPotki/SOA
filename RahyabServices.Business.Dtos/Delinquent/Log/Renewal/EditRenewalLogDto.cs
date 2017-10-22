using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.Renewal{
    public class EditRenewalLogDto : IDto{
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime LegislationDate { get; set; }
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public string AuthorUserName { get; set; }
        [DataMember]
        public string FacilityNumber { get; set; }
        [DataMember]
        public double InterestRate { get; set; }
        [DataMember]
        public string DocumentUrl { get; set; }
        public string UserName
        {
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}