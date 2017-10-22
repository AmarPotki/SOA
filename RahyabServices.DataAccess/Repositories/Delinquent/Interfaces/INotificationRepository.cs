using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.DataAccess.Core.Delinquent;
namespace RahyabServices.DataAccess.Repositories.Delinquent.Interfaces{
    public interface INotificationRepository : IDelinquentRepository<Notification>{
        Task<Notification> GetNotificationByTypeAndCustomerDelinquent(int CustomerDelinquentId, NotificationType type);
        Task<IEnumerable<Notification>> GetNotificationByBranchCode(string branchCode);
        Task<bool> IsExistTendDaysLeftType(int CustomerDelinquentId);
    }
}