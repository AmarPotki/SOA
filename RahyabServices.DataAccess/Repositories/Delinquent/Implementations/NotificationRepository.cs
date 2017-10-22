using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Delinquent;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Implementations{
    public class NotificationRepository : DelinquentRepositoryBase<Notification>, INotificationRepository{
        public NotificationRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
        public async Task<Notification> GetNotificationByTypeAndCustomerDelinquent(int CustomerDelinquentId,
            NotificationType type){
            return
                await
                    QueryAsync(
                        async f =>
                            await
                                f.FirstOrDefaultAsync(
                                    x =>
                                        x.IsDone == false && x.CustomerDelinquentId == CustomerDelinquentId &&
                                        x.NotificationType == type));
        }
        public async Task<bool> IsExistTendDaysLeftType(int CustomerDelinquentId){
            return
                await
                    QueryAsync(
                        async f =>
                            await
                                f.AnyAsync(x=>x.CustomerDelinquentId==CustomerDelinquentId && x.NotificationType ==NotificationType.TenDaysLeft));
        }
        public async Task<IEnumerable<Notification>> GetNotificationByBranchCode(string branchCode){
            return
                await
                    QueryAsync(
                        async f =>
                            await
                                f.Include("CustomerDelinquent").Where(
                                    x =>
                                        x.IsDone == false && x.CustomerDelinquent.BranchCode == branchCode)
                                    .ToListAsync());
        }
    }
}