namespace RahyabServices.Business.Domain.Models.Bank{
    public class RptftGuarantee
    {
        public string ContractCode { get; set; }
        public string GuaranteeStatus { get; set; }
        public RPTFT Rptft { get; set; }
        public Guarantee Guarantee { get; set; } 
    }
}