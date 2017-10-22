using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Delinquent.Log{
    public class RequestImpunityForCrimesLog : LogBase{
        [Column("Description")]
        public string Description { get; set; }
        [Column("IsApprove")]
        public bool? IsApprove { get; set; }
        [Column("InterestRate")]
        public double InterestRate { get; set; }
        [Column("DocumentPath")]
        public string DocumentUrl { get; set; }
        [Column("AllowEdit")]
        public bool AllowEdit { get; set; }
        [Column("SharpointItemId")]
        public int SharpointItemId { get; set; }
        [Column("ImpunityAmount")]
        public decimal ImpunityAmount { get; set; }
    }
}