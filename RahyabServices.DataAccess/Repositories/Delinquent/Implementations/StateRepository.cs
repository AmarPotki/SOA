using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Implementations{
    public class StateRepository : DelinquentRepositoryBase<DelinquentState>, IStateRepository{
        public StateRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
        public IEnumerable<IState> GetExpiredEvents(){
            return Query(q => q.Where(o => o.ExpireDate == DateTime.Now).ToList());
        }
        public async Task<IEnumerable<IState>> GetExpiredEventsAsync(){
            return
                await
                    QueryAsync(async q => await q.Where(o => o.ExpireDate.Value.Date == DateTime.Now.Date).ToListAsync());
        }
        public async Task<bool> IsExist(int customerDelinquentId, int stateId){
            return
                await
                    QueryAsync(
                        async q =>
                            await
                                q.AnyAsync(o => o.Id == stateId && o.HistoryCustomerDelinquentId == customerDelinquentId));
        }
    }
}