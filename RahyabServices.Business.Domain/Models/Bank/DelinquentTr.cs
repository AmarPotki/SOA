using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank{
    //transaction haye tashilat
    [Table("AGLTRNSC")]
    public class DelinquentTr : IBankEntity{
        public string BranchGiven { get; set; }
        public string BranchTrans { get; set; }
        public string TransDate { get; set; }
        public string TraceCode { get; set; }
        public long TransAmount { get; set; }
        public string Contract { get; set; }
        public string ActionCode { get; set; }
        public string LoanReturnedStatus { get; set; }
        public string HisDate { get; set; }
        public string InstructionCode { get; set; }
        [Column("CUSTNO")]
        public string Custno { get; set; }
        [Key]
        [Column("ID")]
        public long Id { get; set; }
    }

}