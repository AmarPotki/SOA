using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RahyabServices.Business.Domain.Models.Bank
{
    [Table("RPTFT038")]
    public class RptftBondDelinquent : IBankEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Branch")]
        [StringLength(4)]
        public string BranchCode { get; set; }
        [Column("Collat_Value")]
        public decimal CollatValue { get; set; }
        [Column("Collat_Date")]
        public int CollatDate { get; set; }
        [Column("Contract")]
        public string Contract { get; set; }
        [Column("Collat_NO")]
        public int CollatNo { get; set; }
    }
}