using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank{
     [Table("RPTFT055")]
    public class GuaranteeDetail : IBankEntity{
        public long Id { get; set; }
        [Column("Contract")]
        public string ContractCode { get; set; }
        public string HisDate { get; set; }
        [Column("Payment_Date")]
        public string PaymentDate { get; set; }
        //nerkh jarime
        public string Jarimeh { get; set; }
    }
}