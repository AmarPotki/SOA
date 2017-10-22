namespace RahyabServices.Business.Domain.Models.Bank{
    public class LastBalDetail{
        public string CustomerNumber { get; set; }
        public int TotalAccountNumber { get; set; }
        public long? RemainingAmountCurrent { get; set; }
        public string HisDate { get; set; }
    }
}