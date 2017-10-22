using System;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Factories.Delinquent.Interfaces;
using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.Business.Domain.Factories.Delinquent.Implementations{
    public class CustomerDelinquentFactory : ICustomerDelinquentFactory{
        public CustomerDelinquent Create(string branchCode, string branchName, DateTime maturityDate, DateTime startDate,
            string customerNumber, string status, string contractCode, string historyDate, string mobileNumber,
            bool isArchived, decimal approvedAmount, double interestRate, decimal remainingPenalty, string resourceId,
            string contractDescription, ContractType contractType, string fullName){
            return new CustomerDelinquent
            {
                BranchCode = branchCode,
                BranchName = branchName,
                MaturityDate = maturityDate,
                StartDate = startDate,
                CustomerNumber = customerNumber,
                Status = status,
                ContractCode = contractCode,
                HistoryDate = historyDate,
                MobileNumber = mobileNumber,
                IsArchived = isArchived,
                ApprovedAmount = approvedAmount,
                InterestRate = interestRate,
                RemainingPenalty = remainingPenalty,
                BankType = resourceId == null ? BankType.BankIran : BankType.Abis,
                ContractDescription = contractDescription,
                ContractType = contractType,
                FullName = fullName
            };
        }
        public async Task<CustomerDelinquent> CreateAsync(string branchCode, string branchName, DateTime maturityDate,
            DateTime startDate, string customerNumber, string status, string contractCode, string historyDate,
            string mobileNumber, bool isArchived, decimal approvedAmount, double interestRate, decimal remainingPenalty,
            string resourceId, string contractDescription, ContractType contractType, string fullName){
            return
                await
                    Task.Run(
                        () =>
                            Create(branchCode, branchName.Trim(), maturityDate, startDate, customerNumber, status,
                                contractCode,
                                historyDate, mobileNumber, isArchived, approvedAmount, interestRate, remainingPenalty,
                                resourceId, contractDescription, contractType, fullName));
        }
    }
}