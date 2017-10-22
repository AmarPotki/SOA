namespace RahyabServices.Business.Domain.Models.Bank{
    public class BranchDelinquentReport
    {
        public string BranchCode { get; set; }
        public long Amount { get; set; }
        public string Contract { get; set; }
    }
}