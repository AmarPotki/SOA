using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core.Bank;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces{
    public interface IRptftGuarantorRepository : IBankRepository<RptftGuarantor>{
        Task<IEnumerable<RptftGuarantor>> GetGuarantor(string contractCode);
    }
}