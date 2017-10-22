using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank{
    [Table("View_RunPackage_Limited")]
    public class Monitor :IBankEntity{
        public long Id { get; set; }
        [Column("rundate")]
        public DateTime RunDate { get; set; }
        public string FDate { get; set; }
        public string PckName { get; set; }

    }
}