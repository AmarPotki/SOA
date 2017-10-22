using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank
{
    [Table("CUSTINFO")]
    public class CustomerInfo : IBankEntity
    {
        [Column("CUSTNO")]
        [StringLength(10)]
        public string CustomerNumber { get; set; }
        [Column("CUSTYPE")]
        public string CustomerType { get; set; }
        [Column("NATCD")]
        public string Nationality { get; set; }
        [Column("BRANCH")]
        [StringLength(4)]
        public string BranchCode { get; set; }
        [Column("DTBRTH")]
        public string BirthDate { get; set; }
        [Column("IDNUMBER")]
        public string NationalNumber { get; set; }
        [Column("SEXCODE")]
        [StringLength(50)]
        public string Sex { get; set; }
        [Column("CUSENGN")]
        public string CustomerGenerator { get; set; }
        [Column("ARBNME")]
        public string FullName { get; set; }
        [Column("FATHER-NAME")]
        public string FatherName { get; set; }
        [Column("ISSUED-TOWN")]
        public string IssuedTown { get; set; }
        [Column("SDRCODE")]
        [StringLength(50)]
        public string SDRCode { get; set; }
        [Column("ISSUED-DATE")]
        public string IssuedDate { get; set; }
        [Column("ECONOMIC-CODE")]
        [StringLength(10)]
        public string EconomicCode { get; set; }
        [Column("COMPGNO")]
        public string CompanyNumber { get; set; }
        [Column("FAX-NUMBER")]
        [StringLength(50)]
        public string FaxNumber { get; set; }
        [Column("ADDRS1")]
        public string Address1 { get; set; }
        [Column("ADDRS2")]
        public string Address2 { get; set; }
        [Column("ZIPCODE")]
        public string ZipCode { get; set; }
        [Column("TELNO")]
        public string Telephone { get; set; }
        [NotMapped]
        public string FirstName => FullName.Substring(33).Trim();
        [NotMapped]
        public string LastName => FullName.Substring(0, 33).Trim();
        [NotMapped]
        public string FullNameManaged => $"{FirstName} {LastName}";
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("IDNUMBER-CORP")]
        [StringLength(255)]
        public string CompanyId { get; set; }
    }
}