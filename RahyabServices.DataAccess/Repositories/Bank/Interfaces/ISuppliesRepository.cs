using System.Data;
using System.Threading.Tasks;
namespace RahyabServices.DataAccess.Repositories.Bank.Interfaces{
    public interface ISuppliesRepository{
        Task<DataSet> GetAccountOwners(string account);
      
    }
}