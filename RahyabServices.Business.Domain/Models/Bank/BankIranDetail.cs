using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Bank{
    [Table("RPTFT040")]
    public class BankIranDetail : IBankEntity{
        [Column("Contract")]
        public string ContractCode { get; set; }
        [Column("Branch")]
        public string BranchCode { get; set; }
        [Column("MANDEJARI")]
        public decimal MandeJari { get; set; }
        [Column("MANDEAGHSAT_GHERJARI")]
        public decimal MandeAghsatGherjari { get; set; }
        [Column("MANDESUDE_GHERJARI")]
        public decimal Mandesudegherjari { get; set; }
        [Column("MANDESUDE_JARI")]
        public decimal MandesudeJari { get; set; }
        [Column("MANDEVAJHELTEZAMDARYAFTI")]
        public string Mandevajheltezamdaryafti { get; set; }
        [Column("CODESARFASL_GHERJARI")]
        public string CodeSarFaslGherjari { get; set; }
        public string HisDate { get; set; }
        [Key]
        [Column("ID")]
        public long Id { get; set; }
    }
}