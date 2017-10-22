using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Delinquent.Contracts{
    [DataContract]
    public class CustomerDelinquentDto : IDto{
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public int CustomerDelinquentId { get; set; }
        [DataMember]
        public string BranchCode { get; set; }
        [DataMember]
        public string OldBranchCode { get; set; }
        [DataMember]
        public string BranchName { get; set; }
        [DataMember]
        public string OldBranchName { get; set; }
        [DataMember]
        public string MaturityDate { get; set; }
        [DataMember]
        public string StartDate { get; set; }
        [DataMember]
        public string CustomerNumber { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ContractCode { get; set; }
        [DataMember]
        public string HistoryDate { get; set; }
        [DataMember]
        public bool IsArchived { get; set; }
        [DataMember]
        public decimal ApprovedAmount { get; set; }
        [DataMember]
        public double InterestRate { get; set; }
        [DataMember]
        public decimal RemainingPenalty { get; set; }
        [DataMember]
        public int BankType { get; set; }
        [DataMember]
        public string ContractType { get; set; }
        [DataMember]
        public string ContractDescription { get; set; }
        [DataMember]
        public string GuaranteeStatus { get; set; }
        [DataMember]
        public decimal Remaining { get; set; }
        // Mandeh Az Soud
        [DataMember]
        public decimal RemainingProfit { get; set; }
        //Kole Bedehi
        [DataMember]
        public decimal DebitBalance { get; set; }
        [DataMember]
        public decimal MandehJary { get; set; }
        [DataMember]
        public decimal MandehGheirJari { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string LastAction { get; set; }
        [DataMember]
        public string PaymentDate { get; set; }
        //nerkh Jarime ZemanatName
        [DataMember]
        public string Jarimeh { get; set; }
        [DataMember]
        //az mahale bedehkaran
        public decimal DebitorAmount { get; set; }
    }
}