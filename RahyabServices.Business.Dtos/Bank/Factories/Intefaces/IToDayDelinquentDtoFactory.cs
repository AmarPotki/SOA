namespace RahyabServices.Business.Dtos.Bank.Factories.Intefaces
{
    public interface IToDayDelinquentDtoFactory
    {
        ToDayDelinquentDto Create(string branchCode, string branchName); 
    }
}