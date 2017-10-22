using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank{
    [Table("RPTFT031")]
    public class AbisDetail:IBankEntity{
        [Column("Contract")]
        public string ContractCode { get; set; }
        [Column("Branch")]
        public string BranchCode { get; set; }
        //old is decimal
        [Column("Remaining_Amount")]
        public long RemainingAmount { get; set; }
        [Column("OverDue1")]
        public decimal OverDue1 { get; set; }
        [Column("OverDue2")]
        public decimal OverDue2 { get; set; }
        [Column("OverDue3")]
        public decimal OverDue3 { get; set; }
        [Column("SoudeOverDue1")]
        public double? SoudeOverDue1 { get; set; }
        [Column("SoudeOverDue2")]
        public double? SoudeOverDue2 { get; set; }
        [Column("SoudeOverDue3")]
        public double? SoudeOverDue3 { get; set; }
        [Column("SoudeTashilateJari")]
        public double? SoudeTashilateJari { get; set; }
        public string HisDate { get; set; }
        [Key]
        [Column("ID")]
        public long Id { get; set; }

    }
}