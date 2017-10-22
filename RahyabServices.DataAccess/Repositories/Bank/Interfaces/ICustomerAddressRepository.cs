using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core.Bank;

namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces
{
    public interface ICustomerAddressRepository : IBankRepository<CustomerAddress>
    {
        CustomerAddress GetByCustomerNumber(string customerNumber);
        Task<CustomerAddress> GetByCustomerNumberAsync(string customerNumber);
        //string GetCellPhone(string customerId);
        //Task<string> GetCellPhoneAsync(string customerId);
    }
}
