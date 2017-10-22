using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RahyabServices.Business.Domain.Models.Bank
{
    [Table("RPTFT037")]
    public class RptftBond : IBankEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Branch")]
        [StringLength(4)]
        public string BranchCode { get; set; }
        [Column("Collat_Status")]
        public int CollatStatus { get; set; }
        [Column("ArzesheTazmin")]
        public decimal ArzesheTazmin { get; set; }
        [Column("TarikhSabtTazmin")]
        public string TarikhSabtTazmin { get; set; }
        [Column("Collat_NO ")]
        public int CollatNo { get; set; }
        [Column("Collat_Type")]
        public int CollatType { get; set; }
    }
}