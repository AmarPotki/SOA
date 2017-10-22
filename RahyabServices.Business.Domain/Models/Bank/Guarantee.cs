using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank{
    [Table("RPTFT068")]
    public class Guarantee : IBankEntity{
        [Column("Contract")]
        public string ContractCode { get; set; }
        public string HisDate { get; set; }
        [Column("Status_Desc")]
        public string StatusDesc { get; set; }
        //az mahale bedehkar
        [Column("Debitor_Amount")]
        public decimal DebitorAmount { get; set; }
        public long Id { get; set; }
    }
}