using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.DataAccess.Core.Delinquent;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Interfaces{
    public interface IStateRepository : IDelinquentRepository<DelinquentState>{
        IEnumerable<IState> GetExpiredEvents();
        Task<IEnumerable<IState>> GetExpiredEventsAsync();
        Task<bool> IsExist(int customerDelinquentId, int stateId);
    }
}