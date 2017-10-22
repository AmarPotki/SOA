using System;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
namespace RahyabServices.Business.Domain.Factories.Delinquent.Interfaces{
    public interface ICustomerDelinquentFactory
    {
        CustomerDelinquent Create(string branchCode, string branchName, DateTime maturityDate, DateTime startDate, string customerNumber, string status, string contractCode, string historyDate, string mobileNumber, bool isArchived, decimal approvedAmount, double interestRate, decimal remainingPenalty, string resourceId, string contractDescription, ContractType contractType, string s);
        Task<CustomerDelinquent> CreateAsync(string branchCode, string branchName, DateTime maturityDate, DateTime startDate, string customerNumber, string status, string contractCode, string historyDate, string mobileNumber, bool isArchived, decimal approvedAmount, double interestRate, decimal remainingPenalty, string resourceId, string contractDescription, ContractType contractType, string fullName);
    }
}