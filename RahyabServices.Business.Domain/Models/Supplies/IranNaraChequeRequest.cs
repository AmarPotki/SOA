using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.Supplies{
    [Table("tbl_ABChequeRequest")]
    public class IranNaraChequeRequest : IIranaraEntity  {
        public string CustomerRequestNumber { get; set; }
        public long BranchRequestNumber { get; set; }
        public string SendingDate { get; set; }
        public string BranchCode { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string CheckbookCode { get; set; }
        public string SendingBranchCode { get; set; }
        public string Iban { get; set; }
        public string User { get; set; }
        public int? ChequeCnt { get; set; }
        public string MelliCode { get; set; }
        public long Id { get; set; }
        public bool IsPrint { get; set; }
    }
}