using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Supplies{
    [Table("tbl_ABRequestSerialID")]
    public class RequestSerialId : IIranaraEntity {

        public string CustomerRequestNumber { get; set; }
        [Column("SerialID")]
        public string SerialId { get; set; }
        public string Seri { get; set; }
        public string Serial { get; set; }
        public long Id { get; set; }
        public IranNaraChequeRequest IranNaraChequeRequest { get; set; }
        public long IranNaraChequeRequestId { get; set; }
        public RequestSerialId SetChequeRequestId(long id){
            IranNaraChequeRequestId = id;
            return this;
        }
    }
}