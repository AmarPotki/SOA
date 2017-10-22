using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RahyabServices.Business.Domain.Models.Delinquent.Log
{
    public class NoticeLog : LogBase
    {
        [Column("DocumentPath")]
        public string DocumentUrl { get; set; }
        [Column("LetterNumber")]
        public string LetterNumber { get; set; }
        public DateTime LetterDate { get; set; }
        [Column("Description")]
        public string Description { get; set; }
    }
}