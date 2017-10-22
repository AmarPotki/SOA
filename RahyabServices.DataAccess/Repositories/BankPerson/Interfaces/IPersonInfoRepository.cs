using System.Collections.Generic;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.BankPerson;
namespace RahyabServices.DataAccess.Repositories.BankPerson.Interfaces{
    public interface IPersonInfoRepository{
        Task<PersonInfo> GetPersonInfo(int personelId);
        Task<PersonAbUser> GetPersonAbUser(string userName);
        Task<string> GetBranchManagerUserName(string branchCode);
        Task<string> GetManagerUserName(string workStationId);
        Task<bool> IsValidUserName(string userName);
        Task<IEnumerable<PersonInfo>> GetAllActive();
        Task<IEnumerable<PersonInfo>> GetAllDeActive();
        Task<IEnumerable<WorkSection>> GetWorkSections();
    }
}