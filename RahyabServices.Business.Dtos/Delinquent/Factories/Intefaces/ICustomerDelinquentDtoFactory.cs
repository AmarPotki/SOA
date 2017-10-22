using System;
using System.Threading.Tasks;

using RahyabServices.Business.Dtos.Delinquent.Contracts;

namespace RahyabServices.Business.Dtos.Delinquent.Factories.Intefaces
{
    public interface ICustomerDelinquentDtoFactory
    {
        CustomerDelinquentDto Create(string branchCode, string branchName, string maturityDate, string startDate, string customerNumber, string status, string contractCode, string historyDate, bool isArchived, decimal approvedAmount, double interestRate, decimal remainingPenalty, string contractDescription);

        Task<CustomerDelinquentDto> CreateAsync(string branchCode, string branchName, string maturityDate, string startDate, string customerNumber,
            string status, string contractCode, string historyDate, bool isArchived, decimal approvedAmount, float interestRate, decimal remainingPenalty, string contractDescription);
    }
}