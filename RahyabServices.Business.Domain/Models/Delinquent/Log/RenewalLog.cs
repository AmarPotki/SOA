using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Delinquent.Log{
    public class RenewalLog : LogBase{
        [Column("LegislationDate")]
        public DateTime LegislationDate { get; set; }
        [Column("InterestRate")]
        public double InterestRate { get; set; }
        [Column("DocumentPath")]
        public string DocumentUrl { get; set; }
        [Column("FacilityNumber")]
        public string FacilityNumber { get; set; }
    }
}