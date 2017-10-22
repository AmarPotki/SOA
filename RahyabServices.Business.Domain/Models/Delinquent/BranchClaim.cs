using System;
namespace RahyabServices.Business.Domain.Models.Delinquent
{
    public class BranchClaim:IDelinquentEntity{
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public double TotalRemainingAmount { get; set; }
        public double TotalApprovedAmount { get; set; }
        public double TotalCount { get; set; }
        public double DebtRemainingAmount { get; set; }
        public double DebtApprovedAmount { get; set; }
        public double DebtCount { get; set; }
        public Branch Branch { get; set; }
        public int BranchId { get; set; }
        public BranchClaim SetBranchId(int id){
            BranchId = id;
            return this;
        }
    }
}