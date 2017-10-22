using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank{
    [Table("ACTINFO")]
    public class AccountInfo : IBankEntity{
        [Column("ACNO")]
        [StringLength(255)]
        public string Accountnumber { get; set; }
        [Column("CUSTNO")]
        [StringLength(10)]
        public string CustomerNumber { get; set; }
        [Column("DATEOPN")]
        public string OpenDate { get; set; }
        [Column("OPNBR")]
        [StringLength(4)]
        public string OpenerBranchCode { get; set; }
        [Key]
        [Column("ID")]
        public long Id { get; set; }
    }
}