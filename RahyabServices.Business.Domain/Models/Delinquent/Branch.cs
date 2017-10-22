namespace RahyabServices.Business.Domain.Models.Delinquent
{
    public class Branch:IDelinquentEntity{
        public int Id { get; set; }
        public string Code { get; set; }
        public string OldCode { get; set; }
        // shobe ,modiriate khadamat banki,...
        public int Level { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string CellPhone { get; set; }
        public string BossName { get; set; }
        public BranchRate BranchRate { get; set; }
        public Branch Parent { get; set; }
        public int? ParentId { get; set; }
        //modiriat manabe va khadamat banki
        public int? BankServiceId { get; set; }
    }
}