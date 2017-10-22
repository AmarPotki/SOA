using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RahyabServices.Business.Domain.Models.Delinquent.Log
{
    public class SplitLog : LogBase
    {
        [Column("LegislationDate")]
        public DateTime LegislationDate { get; set; }
        [Column("Count")]
        public int Count { get; set; }
        [Column("InterestRate")]
        public double InterestRate { get; set; }
        [Column("DocumentPath")]
        public string DocumentUrl { get; set; }
        [Column("StartDate")]
        public DateTime StartDate { get; set; }
        [Column("ApproverPersonnelCode")]
        public string ApproverPersonnelCode { get; set; }
        [Column("AllowEdit")]
        public bool AllowEdit { get; set; }
        [Column("BreakTime")]
        public int BreakTime { get; set; }
        [Column("RefundsType")]
        public int RefundsType { get; set; }
        [Column("DepositAmount")]
        public decimal DepositAmount { get; set; }
        [Column("ApplyLegislatinAfterDueDate")]
        public bool ApplyLegislatinAfterDueDate { get; set; }

    }
}