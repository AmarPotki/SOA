using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RahyabServices.Business.Domain.Models.Bank
{
    [Table("RPTFT067")]
    public class RptftGuarantor : IBankEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Contract")]
        public string Contract { get; set; }
        [Column("Customer_Code")]
        public string CustomerCode { get; set; }
        [Column("Guaranty_Remaining")]
        public decimal GuarantyRemaining { get; set; }
    }
}