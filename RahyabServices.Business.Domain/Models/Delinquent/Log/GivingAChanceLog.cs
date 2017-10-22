using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RahyabServices.Business.Domain.Models.Delinquent.Log
{
    public class GivingAChanceLog : LogBase
    {
        [Column("LegislationDate")]
        public DateTime LegislationDate { get; set; }
        // مدت امهال به ماه
        [Column("Count")]
        public int Count { get; set; }

        [Column("DocumentPath")]
        public string DocumentUrl { get; set; }

        [Column("ExpireDate")]
        public DateTime ExpireDate { get; set; }
        [Column("ApproverPersonnelCode")]
        public string ApproverPersonnelCode { get; set; }
        [Column("AllowEdit")]
        public bool AllowEdit { get; set; }
        [Column("DepositAmount")]
        public decimal DepositAmount { get; set; }

    }
}