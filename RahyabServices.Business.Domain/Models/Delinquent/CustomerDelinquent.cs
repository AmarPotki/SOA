using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
namespace RahyabServices.Business.Domain.Models.Delinquent
{
    public class CustomerDelinquent : IDelinquentEntity{
        public string FullName { get; set; }
        public string BranchCode { get; set; }
       
        //code shobe ghadimi va edghami
        public string OldBranchCode { get; set; }
        public string OldBranchName { get; set; }
        public string BranchName { get; set; }
        public DateTime MaturityDate { get; set; }
        public DateTime StartDate { get; set; }
        public ContractType ContractType { get; set; }
        //zir no ra tozih midahad
        public string ContractDescription { get; set; }
        public string CustomerNumber { get; set; }
        public string Status { get; set; }
        //vaziat ZemanatName agar Tashil Zemanatname bashad
        public string GuaranteeStatus { get; set; }
        // tarikh pardakht zemanatname
        public string PaymentDate { get; set; }
        //nerkh Jarime ZemanatName
        public string Jarimeh { get; set; }
        //az mahale bedehkaran
        public decimal DebitorAmount { get; set; }
        public string ContractCode { get; set; }
        public string HistoryDate { get; set; }
        public string MobileNumber { get; set; }
        public bool IsArchived { get; set; }
        public decimal ApprovedAmount { get; set; }
        public double InterestRate { get; set; }
        // Mandeh az Jarime 
        public decimal RemainingPenalty { get; set; }
        //Mandeh Az Asl
        public decimal Remaining { get; set; }
        // Mandeh Az Soud
        public decimal RemainingProfit { get; set; }
        /// <summary>
        /// Kole Bedehi
        /// </summary>
        public decimal DebitBalance { get; set; }
        public decimal MandehJari { get; set; }
        public decimal MandehGheireJary { get; set; }
        public BankType BankType { get; set; }
        public DelinquentState CurrentState { get; set; }
        [ForeignKey("CurrentState")]
        public int? CurrentStateId { get; set; }
        public int? BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<LogBase> LogBases { get; set; }
        public int Id { get; set; }


        public CustomerDelinquent SetState(int stateId)
        {
            CurrentStateId = stateId;
            CurrentState = null;
            return this;
        }
        public CustomerDelinquent SetBranchId(int branchId)
        {
            BranchId = branchId;
            return this;
        }
    }
}