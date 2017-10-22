using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank{
    public class LastBal : IBankEntity{
        public long Id { get; set; }
        [StringLength(10)]
        [Column("CUSTNO")]
        public string CustNumber { get; set; }
        [Column("ACNO")]
        [StringLength(13)]
        public string AcNo { get; set; }
        [Column("Remaining_Amount_Current")]
        public decimal? RemainingAmountCurrent { get; set; }
        [StringLength(6)]
        public string HisDate { get; set; }
    }
}