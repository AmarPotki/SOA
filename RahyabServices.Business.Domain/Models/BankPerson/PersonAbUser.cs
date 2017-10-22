using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.BankPerson{
    [Table("PERSON_AB_USER")]
    public class PersonAbUser : IBankPersonEntity{
        [Key]
        [Column("PERSONEL_ID")]
        public string PersonelId { get; set; }
        [Column("USER_NAME")]
        public string UserName { get; set; }
        [Column("DATE_MODIFY")]
        public string DateModify { get; set; }
        [Column("MODIFIED_BYE")]
        public string ModifiedBye { get; set; }
        [NotMapped]
        public long Id { get; set; }
    }
}