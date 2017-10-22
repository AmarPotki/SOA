using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank{
    [Table("RPTFT053")]
    public class RPTFT : IBankEntity{
        [Column("Branch")]
        [StringLength(4)]
        public string BranchCode { get; set; }
        [Column("Branch_name")]
        [StringLength(50)]
        public string BranchName { get; set; }
        [Column("Maturity_Date")]
        [StringLength(10)]
        public string MaturityDate { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string StartDate { get; set; }
        [Column("CUSTNO")]
        [StringLength(10)]
        public string CustomerNumber { get; set; }
        [Column("Status")]
        [StringLength(50)]
        public string Status { get; set; }
        [Column("Contract")]
        [StringLength(255)]
        public string ContractCode { get; set; }
        [Column("Contract_desc")]
        public string ContrantDescription { get; set; }
        [Column("HisDate")]
        [StringLength(6)]
        public string HistoryDate { get; set; }
        //old decimal
        [Column("Approved_Amount")]
        public long ApprovedAmount { get; set; }
        [Column("Interest_Rate")]
        public double InterestRate { get; set; }
        [Column("Remaining_Penalty", TypeName = "numeric")]
        public decimal RemainingPenalty { get; set; }
        public string ZemanatAmount { get; set; }
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Resource_ID")]
        public string ResourceId { get; set; }
    }
}