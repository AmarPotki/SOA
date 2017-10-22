using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Delinquent.Log{
    public class ChangeStatusLog:LogBase{
        [Column("StatusType")]
        public StatusType StatusType { get; set; }
    }

}