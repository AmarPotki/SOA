using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RahyabServices.Business.Domain.Models.Delinquent.Log
{
    public class ClearingLog : LogBase
    {
        [Column("LegislationDate")]
        public DateTime LegislationDate { get; set; }
        [Column("DocumentPath")]
        public string DocumentUrl { get; set; }
       
        [Column("ApproverPersonnelCode")]
        public string ApproverPersonnelCode { get; set; }
        
        [Column("AllowEdit")]
        public bool AllowEdit { get; set; }

    }
}