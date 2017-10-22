using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Delinquent.Log
{
    public class WrittenNoticeLog : LogBase
    {
        [Column("LetterNumber")]
        public string LetterNumber { get; set; }
        [Column("DocumentPath")]
        public string DocumentUrl { get; set; }
        [Column("WarningType")]
        public string WarningType { get; set; }
        [Column("WarningDate")]
        public DateTime WarningDate { get; set; }
        [Column("Description")]
        public string Description { get; set; }

    }
}