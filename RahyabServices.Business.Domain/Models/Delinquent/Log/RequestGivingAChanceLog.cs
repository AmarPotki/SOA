using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Delinquent.Log{
    public class RequestGivingAChanceLog : LogBase{
        [Column("Description")]
        public string Description { get; set; }
        [Column("IsApprove")]
        public bool? IsApprove { get; set; }
        [Column("LegislationDate")]
        public DateTime LegislationDate { get; set; }

        // مدت امهال به ماه
        [Column("Count")]
        public int Count { get; set; }
        [Column("DocumentPath")]
        public string DocumentUrl { get; set; }
        [Column("ExpireDate")]
        public DateTime ExpireDate { get; set; }
        [Column("AllowEdit")]
        public bool AllowEdit { get; set; }
        [Column("SharpointItemId")]
        public int SharpointItemId { get; set; }
        [Column("DepositAmount")]
        public decimal DepositAmount { get; set; }
    }
}