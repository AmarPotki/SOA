using System;
using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Log.Split{
    [DataContract]
    public class AddSplitLogDto : IDto{
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public string AuthorUserName { get; set; }
        [DataMember]
        public int BreakTime { get; set; }
        [DataMember]
        public int RefundsType { get; set; }
        [DataMember]
        public decimal DepositAmount { get; set; }
        [DataMember]
        public bool ApplyLegislatinAfterDueDate { get; set; }
        [DataMember]
        public string DocumentUrl { get; set; }
        [DataMember]
        public DateTime LegislationDate { get; set; }
        [DataMember]
        public double InterestRate { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        public string UserName{
            get { return AuthorUserName; }
            set { throw new NotImplementedException(); }
        }
    }
}