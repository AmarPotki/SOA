using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core.Bank;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces{
    public interface ICustomerInfoRepository : IBankRepository<CustomerInfo>{
        CustomerInfo GetByCustomerNumber(string customerNumber);
        Task<CustomerInfo> GetByCustomerNumberAsync(string customerNumber);
        bool IsExist(string customerNumber);
        Task<bool> IsExistAsync(string customerNumber);
        Task<IEnumerable<CustomerInfo>> GetCustomersAsync(IEnumerable<string> custmerNumbers);
        Task<IEnumerable<CustomerInfo>> GetCustomersAsync();
    }
}