using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Delinquent.Log{
    public class RequestClearingLog:LogBase{
        [Column("LegislationDate")]
        public DateTime LegislationDate { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("InterestRate")]
        public double InterestRate { get; set; }
        [Column("IsApprove")]
        public bool? IsApprove { get; set; }
        [Column("DocumentPath")]
        public string DocumentUrl { get; set; }
        [Column("TheAmountOfAssessment")]
        public double TheAmountOfAssessment { get; set; }
        [Column("AllowEdit")]
        public bool AllowEdit { get; set; }
        [Column("SharpointItemId")]
        public int SharpointItemId { get; set; }
    }
}