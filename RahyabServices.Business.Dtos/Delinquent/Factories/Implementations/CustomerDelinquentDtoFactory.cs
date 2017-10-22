using System;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Contracts;
using RahyabServices.Business.Dtos.Delinquent.Factories.Intefaces;

namespace RahyabServices.Business.Dtos.Delinquent.Factories.Implementations
{
    public class CustomerDelinquentDtoFactory : ICustomerDelinquentDtoFactory
    {
        public CustomerDelinquentDto Create(string branchCode, string branchName, string maturityDate,
            string startDate, string customerNumber, string status, string contractCode, string historyDate,
            bool isArchived, decimal approvedAmount, double interestRate, decimal remainingPenalty, string contractDescription)
        {
            return new CustomerDelinquentDto
            {
                BranchCode = branchCode,
                BranchName = branchName,
                MaturityDate = maturityDate,
                StartDate = startDate,
                CustomerNumber = customerNumber,
                Status = status,
                ContractCode = contractCode,
                HistoryDate = historyDate,
                IsArchived = isArchived,
                ApprovedAmount = approvedAmount,
                InterestRate = interestRate,
                RemainingPenalty = remainingPenalty,
                ContractDescription = contractDescription
            };
        }

        public async Task<CustomerDelinquentDto> CreateAsync(string branchCode, string branchName, string maturityDate,
            string startDate, string customerNumber, string status, string contractCode, string historyDate,
            bool isArchived, decimal approvedAmount, float interestRate,
            decimal remainingPenalty,string contractDescription)
        {
            return
               await
                   Task.Run(
                       () =>
                           Create(branchCode, branchName, maturityDate, startDate, customerNumber,
                           status, contractCode, historyDate, isArchived, approvedAmount, interestRate, remainingPenalty,contractDescription));
        }
    }
}