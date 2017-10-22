using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Bank;
using RahyabServices.DataAccess.Core.Bank;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces{
    public interface IGuaranteeDetailRepository : IBankRepository<GuaranteeDetail>{

        Task<IEnumerable<GuaranteeDetail>> GetByPersianDate(string persianDate);
    }
}