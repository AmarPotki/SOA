using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.Business.Domain.Models.Sharepoint.BranchMarketing;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.DataAccess.Core.Bank;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces{
    public interface ILastBalRepository : IBankRepository<LastBal>{
        Task<IEnumerable<LastBalDetail>> GetThirtyLastBal(string custNumber);
        Task<decimal> GetLastBal(string customerNumbe);
        Task<IEnumerable<CustomerLastBal>> GetLastBal(string[] customerNumbes,string actionDates);
    }
}