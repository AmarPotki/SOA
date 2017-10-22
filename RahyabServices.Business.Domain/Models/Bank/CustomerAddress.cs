using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank{
    [Table("CUSTADDR")]
    public class CustomerAddress : IBankEntity{
        [Required]
        [Column("CUSTNO")]
        [StringLength(10)]
        public string CustomerNumber { get; set; }
        [Column("HOMPHONE")]
        [StringLength(20)]
        public string HomePhone { get; set; }
        [Column("BUSPHONE")]
        [StringLength(12)]
        public string BusinessPhone { get; set; }
        [Column("NAMADDR")]
        public string Address { get; set; }
        [Column("NEWPOSTCODE")]
        [StringLength(10)]
        public string PostalCode { get; set; }
        [Column("SERIAL-NUMBER")]
        [StringLength(20)]
        public string SerialNumber { get; set; }
        [Column("MOBILE")]
        [StringLength(100)]
        public string MobilePhone { get; set; }
        [Key]
        [Column("ID")]
        public long Id { get; set; }
    }
}