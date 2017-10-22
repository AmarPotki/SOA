using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Delinquent.Contracts;
using RahyabServices.Business.Dtos.Delinquent.Customer;
using absorb = RahyabServices.Business.Dtos.AbsorbResources;

namespace RahyabServices.Business.Services.Intefaces.Bank
{
    public interface ICustomerAccountService
    {
        string GetCustomerMobileNumber(string customerNumber);
        Task<string> GetCustomerMobileNumberAsync(string customerNumber);
        Task<CustomerInformationDto> GetCustomerInformationAsync(GetCustomerInformationDto customerInformationDto);
        string GetSheba(string accountNumber);
        
    }
}